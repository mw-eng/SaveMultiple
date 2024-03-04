using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SaveMultiple.Properties;
using MWComLibCS.Exclusive;
using static MWComLibCS.Exclusive.agPNA835x;
using System.IO;
using System.Text;

namespace SaveMultiple
{
    public partial class frmMain : Form
    {
        private agPNA835x pna;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

#if DEBUG
            Settings.Default.Reset();
#endif
            this.Text += " Ver," + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;
            try { pna = new agPNA835x(); }
            catch
            {
                MessageBox.Show("Connection failed program aborted.", "SaveMutiple ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbCh.Enabled = false;
                gbConf.Enabled = false;
                gbSaveTarg.Enabled = false;
                btSAVE.Enabled = false;
                tbFT.Enabled = false;
                label1.Enabled = false;
                return;
            }
            foreach (uint i in pna.getChannelCatalog())
            {
                ddlCH.Items.Add(i.ToString());
            }
            ddlCH.SelectedIndex = 0;
            foreach (uint i in pna.getPortCatalog())
            {
                clbPT.Items.Add("Port" + i.ToString());
            }

            //Read Settings
            string[] ports = Settings.Default.tp.Split(',');
            if (!string.IsNullOrEmpty(ports[0]))
            {
                for (int i = 0; i < clbPT.Items.Count; i++)
                {
                    foreach (string strBf in ports)
                    {
                        if ("Port" + strBf == clbPT.Items[i].ToString())
                        {
                            clbPT.SetItemChecked(i, true);
                        }
                    }
                }
            }
            rbALL.Checked = true; ddlCH.Enabled = false;
            if (Settings.Default.ch != 0)
            {
                for (int i = 0; i < ddlCH.Items.Count; i++)
                {
                    if (uint.Parse(ddlCH.Items[i].ToString()) == Settings.Default.ch)
                    {
                        ddlCH.SelectedIndex = i;
                        rbSELECT.Checked = true; ddlCH.Enabled = true;
                        break;
                    }
                }
            }
            if (Settings.Default.img) { cbIMG.Checked = true; } else { cbIMG.Checked = false; }
            if (Settings.Default.snp) { cbSNP.Checked = true; } else { cbSNP.Checked = false; }
            if (Settings.Default.trace) { cbTRACE.Checked = true; } else { cbTRACE.Checked = false; }
            if (Settings.Default.sing) { cbSING.Checked = true; } else { cbSING.Checked = false; }
            if (Settings.Default.title != "") { tbFT.Text = Settings.Default.title; } else { tbFT.Text = "multipleDAT"; }
        }

        private void rbALL_CheckedChanged(object sender, EventArgs e)
        {
            ddlCH.Enabled = false;
        }

        private void rbSELECT_CheckedChanged(object sender, EventArgs e)
        {
            ddlCH.Enabled = true;
        }

        private void btCANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSAVE_Click(object sender, EventArgs e)
        {
            this.Hide();
            string dirPath;
            List<SweepMode> trigMODE = new List<SweepMode>();
            uint[] channels;
            uint[] sheets;
            bool fileFLG = false;
            uint[] ports;
            string filePath;
            string message;

            ports = new uint[clbPT.CheckedItems.Count];
            for (int i = 0; i < clbPT.CheckedItems.Count; i++)
            {
                ports[i] = uint.Parse(clbPT.CheckedItems[i].ToString().Replace("Port", ""));
            }
            //Select Folder Dialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please Select Folder";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = Settings.Default.dir;
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                dirPath = fbd.SelectedPath;
            }
            else { this.Show(); return; }

            //Get Channel Lists
            if (rbALL.Checked) { channels = pna.getChannelCatalog(); }
            else { channels = new uint[] { uint.Parse(ddlCH.Text) }; }
            //Get Sheet Lists
            sheets = pna.getSheetsCatalog();

            //File Check
            if (cbIMG.Checked)
            {
                foreach (uint i in sheets)
                {
                    if (System.IO.File.Exists(dirPath + "\\" + tbFT.Text + "_Sheet" + i.ToString() + ".png")) { fileFLG = true; }
                }
            }
            if (cbSNP.Checked)
            {
                foreach (uint ch in channels)
                {
                    if (System.IO.File.Exists(dirPath + "\\" + tbFT.Text + "_CH" + ch.ToString() + ".s" + ports.Length.ToString() + "p")) { fileFLG = true; }
                }
            }
            if (cbTRACE.Checked)
            {
                foreach (uint i in sheets)
                {
                    if (System.IO.File.Exists(dirPath + "\\" + tbFT.Text + "_Sheet" + i.ToString() + ".csv")) { fileFLG = true; }
                }
            }
            if (fileFLG)
            {
                if (MessageBox.Show("The file exists in the specified folder.\nDo you want to overwrite?",
                    "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) { this.Show(); return; }
            }

            //Trigger SET
            if (cbSING.Checked)
            {
                foreach (uint i in channels)
                {
                    trigMODE.Add(pna.getTriggerMode(i));
                    pna.trigSingle(i);
                }
            }

            //Save Screen
            if (cbIMG.Checked)
            {
                foreach (uint sh in sheets)
                {
                    filePath = dirPath + "\\" + tbFT.Text + "_Sheet" + sh.ToString() + ".png";
                    if (System.IO.File.Exists(filePath))
                    {
                        if (!pna.deleteFile(filePath, out message))
                        {
                            if (cbSING.Checked)
                            {
                                for (int i = 0; i < channels.Length; i++)
                                {
                                    pna.SettriggerMode(channels[i], trigMODE[i]);
                                }
                            }
                            if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes) { this.Close(); return; }
                            else { this.Show(); return; }
                        }
                    }
                    pna.selectSheet(sh);
                    if (!pna.saveScreen(filePath, out message))
                    {
                        if (cbSING.Checked)
                        {
                            for (int i = 0; i < channels.Length; i++)
                            {
                                pna.SettriggerMode(channels[i], trigMODE[i]);
                            }
                        }
                        if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            == DialogResult.Yes) { this.Close(); return; }
                        else { this.Show(); return; }
                    }
                }
            }

            //Save SnP
            if (cbSNP.Checked)
            {
                foreach (uint ch in channels)
                {
                    filePath = dirPath + "\\" + tbFT.Text + "_CH" + ch.ToString() + ".s" + ports.Length.ToString() + "p";
                    if (System.IO.File.Exists(filePath))
                    {
                        if (!pna.deleteFile(filePath, out message))
                        {
                            if (cbSING.Checked)
                            {
                                for (int i = 0; i < channels.Length; i++)
                                {
                                    pna.SettriggerMode(channels[i], trigMODE[i]);
                                }
                            }
                            if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes) { this.Close(); return; }
                            else { this.Show(); return; }
                        }
                    }
                    if (!pna.saveSNP(ch, filePath, ports, out message))
                    {
                        if (cbSING.Checked)
                        {
                            for (int i = 0; i < channels.Length; i++)
                            {
                                pna.SettriggerMode(channels[i], trigMODE[i]);
                            }
                        }
                        if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            == DialogResult.Yes) { this.Close(); return; }
                        else { this.Show(); return; }
                    }
                }
            }

            //Save Trace
            if (cbTRACE.Checked)
            {
                foreach (uint sh in sheets)
                {
                    filePath = dirPath + "\\" + tbFT.Text + "_Sheet" + sh.ToString() + ".csv";
                    if (System.IO.File.Exists(filePath))
                    {
                        if (!pna.deleteFile(filePath, out message))
                        {
                            if (cbSING.Checked)
                            {
                                for (int i = 0; i < channels.Length; i++)
                                {
                                    pna.SettriggerMode(channels[i], trigMODE[i]);
                                }
                            }
                            if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes) { this.Close(); return; }
                            else { this.Show(); return; }
                        }
                    }
                    //Select Sheet
                    pna.selectSheet(sh);
                    //Get Trace DAT
                    List<ChartDAT> dat = new List<ChartDAT>();
                    foreach (uint win in pna.getWindowCatalog(sh))
                    {
                        List<TraceDAT> trace = new List<TraceDAT>();
                        foreach (uint tra in pna.getTraceCatalog(win))
                        {
                            pna.selectTrace(win, tra);
                            uint ch = pna.getSelectChannel();
                            uint num = pna.getSelectMeasurementNumber();
                            string x = pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":X:AXIS:UNIT?");
                            string y = pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":PAR?");
                            y += "_" + pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":FORM?");
                            y += "_" + pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":X:AXIS:UNIT?");
                            string mem = pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":MATH:FUNC?");
                            if (mem.ToUpper() != "NORM")
                            {
                                y += "@" + mem + "[MEM]";
                            }
                            string[] valx = pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":X?").Trim().Split(',');
                            string[] valy = pna.getSCPIcommand("CALC" + ch.ToString() + ":MEAS" + num.ToString() + ":Y?").Trim().Split(',');
                            trace.Add(new TraceDAT("CH" + ch.ToString(), x, y, valx, valy));
                        }
                        dat.Add(new ChartDAT("Win" + win.ToString(), trace.ToArray()));
                    }
                    //Write CSV Data
                    using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

                        sw.WriteLine("\"SaveMultiple Ver," + fvi.ProductVersion + "\"");
                        sw.WriteLine(fvi.LegalCopyright);
                        sw.WriteLine();
                        string strBF1 = "";
                        string strBF2 = "";
                        string strBF3 = "";
                        int cnt = 0;
                        foreach (ChartDAT chart in dat)
                        {
                            strBF1 += chart.WindowNumber + ",,";
                            for (int i = 0; i < chart.Trace.Length - 1; i++)
                            {
                                strBF1 += "," + ",";
                            }
                            foreach (TraceDAT trace in chart.Trace)
                            {
                                strBF2 += trace.ChannelNumber + ",,";
                                strBF3 += trace.AxisX + "," + trace.AxisY + ",";
                                if (cnt < trace.ValueX.Length) { cnt = trace.ValueX.Length; }
                                if (cnt < trace.ValueY.Length) { cnt = trace.ValueY.Length; }
                            }
                        }
                        sw.WriteLine(strBF1.Trim(','));
                        sw.WriteLine(strBF2.Trim(','));
                        sw.WriteLine(strBF3.Trim(','));

                        for (int i = 0; i < cnt; i++)
                        {
                            strBF1 = "";
                            foreach (ChartDAT chart in dat)
                            {
                                foreach (TraceDAT trace in chart.Trace)
                                {
                                    if (trace.ValueX.Length > i) { strBF1 += trace.ValueX[i]; }
                                    strBF1 += ",";
                                    if (trace.ValueY.Length > i) { strBF1 += trace.ValueY[i]; }
                                    strBF1 += ",";
                                }
                            }
                            sw.WriteLine(strBF1.Trim(','));
                        }
                        sw.Close();
                    }
                }
            }


            //End processing
            //Settings.Default.ch = rbALL.Checked;
            //if (rbALL.Checked) { Settings.Default.ch = ""; }
            if (rbALL.Checked) { Settings.Default.ch = 0; }
            else { Settings.Default.ch = uint.Parse(ddlCH.SelectedItem.ToString()); }
            Settings.Default.img = cbIMG.Checked;
            Settings.Default.snp = cbSNP.Checked;
            Settings.Default.trace = cbTRACE.Checked;
            Settings.Default.sing = cbSING.Checked;
            Settings.Default.title = tbFT.Text;
            Settings.Default.dir = dirPath;
            string strPorts = "";
            foreach (uint uintBF in ports)
            {
                strPorts += uintBF.ToString() + ",";
            }
            Settings.Default.tp = strPorts.Trim(',');
            Settings.Default.Save();
            //End Message
            if (MessageBox.Show("Multiple saves completed.\nDo you want to open the destination folder?\nDir>" + dirPath,
                "SUCCESS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(dirPath);
            }
            this.Close();
        }


        #region GUI Events
        private void cbIMG_CheckedChanged(object sender, EventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
        }

        private void cbSNP_CheckedChanged(object sender, EventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
            if (cbSNP.Checked)
            {
                if (clbPT.CheckedItems.Count == 0) { btSAVE.Enabled = false; }
                gbSelPort.Enabled = true;
            }
            else { gbSelPort.Enabled = false; }
        }

        private void cbTRACE_CheckedChanged(object sender, EventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
        }

        private void tbFT_TextChanged(object sender, EventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
        }

        private void tbPT_TextChanged(object sender, EventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
        }

        private void clbPT_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            btSAVE.Enabled = enabSaveButton();
            if (e.CurrentValue == CheckState.Checked && clbPT.CheckedItems.Count == 1) { btSAVE.Enabled = false; }
            else { btSAVE.Enabled = true; }
        }

        private void btALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPT.Items.Count; i++) { clbPT.SetItemChecked(i, true); }
        }

        private void btCLEAR_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPT.Items.Count; i++) { clbPT.SetItemChecked(i, false); }
        }

        private bool enabSaveButton()
        {
            if ((!cbIMG.Checked && (!cbSNP.Checked || (cbSNP.Checked && clbPT.CheckedItems.Count == 0)) && !cbTRACE.Checked) || tbFT.Text == "") { return false; }
            else { return true; }
        }
        #endregion

        #region Structure
        private struct ChartDAT
        {
            public string WindowNumber { get; set; }
            public TraceDAT[] Trace { get; set; }

            public ChartDAT(string winNum, TraceDAT[] trace) { WindowNumber = winNum; Trace = trace; }
        }

        private struct TraceDAT
        {
            public string ChannelNumber { get; set; }
            public string AxisX { get; set; }
            public string AxisY { get; set; }
            public string[] ValueX { get; set; }
            public string[] ValueY { get; set; }
            public TraceDAT(string ch, string x, string y, string[] val_x, string[] val_y)
            {
                ChannelNumber = ch; AxisX = x; AxisY = y; ValueX = val_x; ValueY = val_y;
            }
        }
        #endregion
    }
}
