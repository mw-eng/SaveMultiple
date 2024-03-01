using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SaveMultiple.Properties;
using MWComLibCS.Exclusive;
using static MWComLibCS.Exclusive.agPNA835x;

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
            pna = new agPNA835x();

            foreach (uint i in pna.getChannelCatalog())
            {
                ddlCH.Items.Add(i.ToString());
            }
            ddlCH.SelectedIndex = 0;
            foreach(uint i in pna.getPortCatalog())
            {
                clbPT.Items.Add("Port" + i.ToString());
            }
            for(int i=0;i<clbPT.Items.Count; i++) { clbPT.SetItemChecked(i, true); }

            //Read Settings
            if (Settings.Default.ch) { rbALL.Checked = true; ddlCH.Enabled = false; }
            else { rbSELECT.Checked = false; ddlCH.Enabled = true; }
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
            string dirPath;
            List<SweepMode> trigMODE = new List<SweepMode>();
            uint[] channels;
            uint[] sheets;
            bool fileFLG = false;
            uint[] ports;
            string filePath;
            string message;

            ports = new uint[clbPT.CheckedItems.Count];
            for(int i = 0; i < clbPT.CheckedItems.Count; i++)
            {
                ports[i] = uint.Parse(clbPT.CheckedItems[i].ToString().Replace("Port",""));
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
            else { return; }

            //Get Channel Lists
            if (rbALL.Checked) { channels = pna.getChannelCatalog(); }
            else { channels = new uint[] { uint.Parse(ddlCH.Text) }; }
            //Get Sheet Lists
            sheets = pna.getSheetsCatalog();

            //File Check
            if (cbIMG.Checked)
            {
                foreach(uint i in sheets)
                {
                    if (System.IO.File.Exists(dirPath + "\\" + tbFT.Text + "_Sheet" + i.ToString() + ".png")) { fileFLG = true; }
                }
            }
            if (cbSNP.Checked)
            {
                foreach(uint ch in channels)
                {
                    if (System.IO.File.Exists(dirPath + "\\" + tbFT.Text + "_CH" + ch.ToString() + ".s" + ports.Length.ToString() + "p")) { fileFLG = true; }
                }
            }
            if (cbTRACE.Checked)
            {
                if(System.IO.File.Exists(dirPath + "\\" + tbFT.Text + ".csv")) { fileFLG = true; }
            }
            if (fileFLG)
            {
                if(MessageBox.Show("The file exists in the specified folder.\nDo you want to overwrite?",
                    "Warning",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.Cancel) return;
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
                foreach (uint i in sheets)
                {
                    filePath = dirPath + "\\" + tbFT.Text + "_Sheet" + i.ToString() + ".png";
                    if (System.IO.File.Exists(filePath))
                    {
                        if (!pna.deleteFile(filePath, out message))
                        {
                            if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes) { this.Close(); }
                            else { return; }
                        }
                    }
                    pna.selectSheet(i);
                    if (!pna.saveScreen(filePath, out message))
                    {
                        if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            == DialogResult.Yes) { this.Close(); }
                        else { return; }
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
                            if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                == DialogResult.Yes) { this.Close(); }
                            else { return; }
                        }
                    }
                    if (!pna.saveSNP(ch, filePath,ports, out message))
                    {
                        if (MessageBox.Show(message + "\n\nDo you want to end the process?", "ERROR",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                            == DialogResult.Yes) { this.Close(); }
                        else { return; }
                    }
                }
            }


            //Save Trace
            if (cbTRACE.Checked)
            {
                filePath = dirPath + "\\" + tbFT.Text + ".csv";
            }


            //End processing
            if (cbSING.Checked)
            {
                for(int i = 0; i < channels.Length; i++)
                {
                    pna.SettriggerMode(channels[i], trigMODE[i]);
                }
            }
            Settings.Default.ch = rbALL.Checked;
            Settings.Default.img = cbIMG.Checked;
            Settings.Default.snp = cbSNP.Checked;
            Settings.Default.trace = cbTRACE.Checked;
            Settings.Default.sing = cbSING.Checked;
            Settings.Default.title = tbFT.Text;
            Settings.Default.dir = dirPath;
            Settings.Default.Save();
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
                if(clbPT.CheckedItems.Count == 0) { btSAVE.Enabled = false; }
                clbPT.Enabled = true;
            }
            else { clbPT.Enabled = false; }
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
    }
}
