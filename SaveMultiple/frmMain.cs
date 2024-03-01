﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using AgilentPNA835x;
using SaveMultiple.Properties;
using System.Diagnostics.Eventing.Reader;
using System.Configuration;

namespace SaveMultiple
{
    public partial class frmMain : Form
    {
        private static IApplication app;
        private static IScpiStringParser scpi;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text += " Ver," + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;

            Type typeFromProgID = Type.GetTypeFromProgID("AgilentPNA835x.Application");
            app = (IApplication)Activator.CreateInstance(typeFromProgID);
            scpi = app.ScpiStringParser;

            foreach (uint i in getChannelCatalog())
            {
                ddlCH.Items.Add(i.ToString());
            }
            ddlCH.SelectedIndex = 0;

            //Read Settings
            if (Settings.Default.ch) { rbALL.Checked = true; ddlCH.Enabled = false; }
            else { rbSELECT.Checked = false; ddlCH.Enabled = true; }
            if (Settings.Default.img) { cbIMG.Checked = true; } else { cbIMG.Checked = false; }
            if (Settings.Default.snp) { cbSNP.Checked = true; } else { cbSNP.Checked = false; }
            if (Settings.Default.trace) { cbTRACE.Checked = true; } else { cbTRACE.Checked = false; }
            if (Settings.Default.sing) { cbSING.Checked = true; } else { cbSING.Checked = false; }

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
            List<string> trigMODE = new List<string>();

            uint[] channels;
            if (rbALL.Checked) { channels = getChannelCatalog(); }
            else { channels = new uint[] { uint.Parse(ddlCH.Text) }; }

            //Trigger SET
            if (cbSING.Checked)
            {
                foreach (uint i in channels)
                {
                    trigMODE.Add(getTriggerMode(i));
                    trigSingle(i);
                }
            }

            //Save Screen


            //End processing
            if (cbSING.Checked)
            {
                for(int i = 0; i < channels.Length; i++)
                {
                    SettriggerMode(channels[i], trigMODE[i]);
                }
            }
            Settings.Default.ch = rbALL.Checked;
            Settings.Default.img = cbIMG.Checked;
            Settings.Default.snp = cbSNP.Checked;
            Settings.Default.trace = cbTRACE.Checked;
            Settings.Default.sing = cbSING.Checked;
            Settings.Default.Save();
            this.Close();
        }

        #region private functions

        private string getTriggerMode(uint ch) { return getSCPIcommand("SENS" + ch.ToString() + ":SWE:MODE?"); }

        private void trigSingle(uint ch) { SettriggerMode(ch, "SING"); }
        private void trigHold(uint ch) { SettriggerMode(ch, "HOLD"); }
        private void trigContinuous(uint ch) { SettriggerMode(ch, "CONT"); }

        private void SettriggerMode(uint ch, string trig)
        {
            getSCPIcommand("SENS" + ch.ToString() + ":SWE:MODE " + trig);
            getSCPIcommand("*OPC?");
        }

        /// <summary>Select Sheet</summary>
        /// <param name="sheetID">Sheet ID</param>
        private void selectSheet(uint sheetID)
        {
            uint win = getWindowCatalog(sheetID)[0];
            uint tra = getTraceCatalog()[0];
            getSCPIcommand("DISP:WIND" + win + ":TRAC" + tra + ":SEL");
        }



        public uint[] getTraceCatalog() { return getTraceCatalog(0); }

        public uint[] getTraceCatalog(uint WindowID)
        {
            List<uint> trace = new List<uint>();
            string[] arrBF;
            if (WindowID <= 0) { arrBF = getSCPIcommand("DISP:WIND:CAT?").Split(','); }
            else { arrBF = getSCPIcommand("DISP:WIND" + WindowID.ToString() + ":CAT?").Split(','); }
            foreach (string strBf in arrBF) { trace.Add(uint.Parse(strBf)); }
            return trace.ToArray();
        }

        /// <summary>Get Window Catalog</summary>
        /// <returns>Window List</returns>
        public uint[] getWindowCatalog() { return getWindowCatalog(0); }

        /// <summary>Get Window Catalog</summary>
        /// <param name="sheetID">Sheet ID</param>
        /// <returns>Window List</returns>
        public uint[] getWindowCatalog(uint sheetID)
        {
            List<uint> win = new List<uint>();
            string[] arrBF;
            if (sheetID <= 0) { arrBF = getSCPIcommand("DISP:SHE:CAT?").Split(','); }
            else { arrBF = getSCPIcommand("DISP:SHE" + sheetID.ToString() + ":CAT?").Split(','); }
            foreach (string strBf in arrBF) { win.Add(uint.Parse(strBf)); }
            return win.ToArray();
        }

        /// <summary>Get Sheet Catalog</summary>
        /// <returns>sheet list</returns>
        private uint[] getSheetsCatalog()
        {
            List<uint> sh = new List<uint>();
            string[] arrBF = getSCPIcommand("SYST:SHE:CAT?").Split(',');
            foreach (string strBf in arrBF) { sh.Add(uint.Parse(strBf)); }
            return sh.ToArray();
        }

        /// <summary>Get Channel Catalog</summary>
        /// <returns>channel catalog</returns>
        private uint[] getChannelCatalog()
        {
            List<uint> ch = new List<uint>();
            string[] arrBF = getSCPIcommand("SYST:CHAN:CAT?").Split(',');
            foreach (string strBf in arrBF) { ch.Add(uint.Parse(strBf)); }
            return ch.ToArray();
        }

        /// <summary>Get SCPI Command</summary>
        /// <param name="cmd">Command line</param>
        /// <returns>Command results</returns>
        private string getSCPIcommand(string cmd)
        {
            return scpi.Parse(cmd).Trim('\n').Trim('\"');
        }

        #endregion
    }
}
