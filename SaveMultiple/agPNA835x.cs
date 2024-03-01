using AgilentPNA835x;
using System;
using System.Collections.Generic;

namespace MWComLibCS.Exclusive
{
    /// <summary>AgilentPNA835x Wrapper Library</summary>
    public class agPNA835x
    {
        private IApplication app;
        private IScpiStringParser scpi;

        /// <summary>コンストラクタ</summary>
        public agPNA835x()
        {
            Type typeFromProgID = Type.GetTypeFromProgID("AgilentPNA835x.Application");
            app = (IApplication)Activator.CreateInstance(typeFromProgID);
            scpi = app.ScpiStringParser;
        }
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

    }
}
