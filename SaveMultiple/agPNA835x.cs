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

        #region ENUM
        /// <summary>Sweep Mode enum</summary>
        public enum SweepMode
        {
            /// <summary>Hold</summary>
            HOLD = 1,
            /// <summary>Continuous</summary>
            CONT = 2,
            /// <summary>Groups(SENS:SWE:GRO:COUN)</summary>
            GRO = 3,
            /// <summary>Single</summary>
            SING = 4
        }
        #endregion

        /// <summary>コンストラクタ</summary>
        public agPNA835x()
        {
            Type typeFromProgID = Type.GetTypeFromProgID("AgilentPNA835x.Application");
            app = (IApplication)Activator.CreateInstance(typeFromProgID);
            scpi = app.ScpiStringParser;
        }

        /// <summary>Get Trigger Mode</summary>
        /// <param name="ch">Channel</param>
        /// <returns>Trigger Mode</returns>
        public SweepMode getTriggerMode(uint ch)
        {
            string strBF = getSCPIcommand("SENS" + ch.ToString() + ":SWE:MODE?");
            switch (strBF.ToUpper())
            {
                case "HOLD":
                    return SweepMode.HOLD;
                case "CONT":
                    return SweepMode.CONT;
                case "GRO":
                    return SweepMode.GRO;
                case "SING":
                    return SweepMode.SING;
                default:
                    return SweepMode.CONT;
            }
        }

        /// <summary>Set Trigger Mode</summary>
        /// <param name="ch">Channel</param>
        /// <param name="trig">Trigger Mode</param>
        public void SettriggerMode(uint ch, SweepMode trig)
        {
            getSCPIcommand("SENS" + ch.ToString() + ":SWE:MODE " + trig.ToString());
            getSCPIcommand("*OPC?");
        }

        /// <summary>Set Single Trigger</summary>
        /// <param name="ch"></param>
        public void trigSingle(uint ch) { SettriggerMode(ch, SweepMode.SING); }
        /// <summary>Set HOLD Trigger</summary>
        public void trigHold(uint ch) { SettriggerMode(ch, SweepMode.HOLD); }
        /// <summary>Set Trigger Continuous</summary>
        public void trigContinuous(uint ch) { SettriggerMode(ch, SweepMode.CONT); }

        /// <summary>Select Sheet</summary>
        /// <param name="sheetID">Sheet ID</param>
        public void selectSheet(uint sheetID)
        {
            uint win = getWindowCatalog(sheetID)[0];
            uint tra = getTraceCatalog()[0];
            selectTrace(win, tra);
        }

        /// <summary>Get Trace Catalog</summary>
        /// <returns>Trace Catalog</returns>
        public uint[] getTraceCatalog() { return getTraceCatalog(0); }

        /// <summary>Get Trace Catalog</summary>
        /// <param name="WindowID">Window ID</param>
        /// <returns>Trace Catalog</returns>
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
        public uint[] getSheetsCatalog()
        {
            List<uint> sh = new List<uint>();
            string[] arrBF = getSCPIcommand("SYST:SHE:CAT?").Split(',');
            foreach (string strBf in arrBF) { sh.Add(uint.Parse(strBf)); }
            return sh.ToArray();
        }

        /// <summary>Get Channel Catalog</summary>
        /// <returns>channel catalog</returns>
        public uint[] getChannelCatalog()
        {
            List<uint> ch = new List<uint>();
            string[] arrBF = getSCPIcommand("SYST:CHAN:CAT?").Split(',');
            foreach (string strBf in arrBF) { ch.Add(uint.Parse(strBf)); }
            return ch.ToArray();
        }

        /// <summary>Get Port Catalog</summary>
        /// <returns>Port Catalog</returns>
        public uint[] getPortCatalog()
        {
            List<uint> port = new List<uint>();
            string[] arrBF = getSCPIcommand("SOUR:CAT?").Split(',');
            foreach (string strBf in arrBF) { port.Add(uint.Parse(strBf.Replace("Port", ""))); }
            return port.ToArray();
        }

        public void selectTrace(uint winNum,uint traceNum)
        {
            getSCPIcommand("DISP:WIND" + winNum + ":TRAC" + traceNum + ":SEL");
        }

        public uint getSelectChannel()
        {
            return uint.Parse(getSCPIcommand("SYST:ACT:CHAN?"));
        }

        public uint getSelectMeasurementNumber()
        {
            return uint.Parse(getSCPIcommand("SYST:ACT:MEAS:NUMB?"));
        }

        public bool deleteFile(string filePATH, out string ErrorMessage)
        {
            getSCPIcommand("DISP:CCL");    //Display Error clear
            getSCPIcommand("*CLS");         //status register clear
            try
            {
                getSCPIcommand("MMEM:DEL \"" + filePATH + "\"");   //delete file
            }catch(Exception e){ ErrorMessage = e.Message; return false; }
            string strBF = getSCPIcommand("SYST:ERR?"); //Error check
            string[] strArr = strBF.Split(',');
            if (int.Parse(strArr[0]) != 0)
            {
                ErrorMessage = strArr[1];
                return false;
            }
            else
            {
                ErrorMessage = "SUCCESS";
                return true;
            }
        }

        public bool saveScreen(string filePATH, out string ErrorMessage)
        {
            getSCPIcommand("DISP:CCL");    //Display Error clear
            getSCPIcommand("*CLS");         //status register clear
            getSCPIcommand("HCOPY:FILE \"" + filePATH + "\"");  //save screen file
            string strBF = getSCPIcommand("SYST:ERR?"); //Error check
            string[] strArr = strBF.Split(',');
            if (int.Parse(strArr[0]) != 0)
            {
                ErrorMessage = strArr[1];
                return false;
            }
            else
            {
                ErrorMessage = "SUCCESS";
                return true;
            }
        }
        public bool saveSNP(uint ch, string filePATH, uint[] ports, out string ErrorMessage)
        {
            string portBF = "";
            foreach (uint i in ports) { portBF += "," + i.ToString(); }
            return saveSNP(ch,filePATH,portBF.Trim(','), out ErrorMessage);
        }

        public bool saveSNP(uint ch, string filePATH, string ports, out string ErrorMessage)
        {
            getSCPIcommand("DISP:CCL");    //Display Error clear
            getSCPIcommand("*CLS");         //status register clear
            //CH Trace Select
            string[] traArr = getSCPIcommand("CALC" + ch.ToString() + ":PAR:CAT?").Split(',');
            if (traArr.Length < 2) { ErrorMessage = "Trace does not exist"; return false; }
            getSCPIcommand(":CALC" + ch.ToString() + ":PAR:SEL " + traArr[0]);         //Select trace
            getSCPIcommand(":CALC" + ch.ToString() + ":DATA:SNP:PORT:SAVE \"" + ports + "\",\"" + filePATH + "\"");    //SaveSNP
            string strBF = getSCPIcommand("SYST:ERR?"); //Error check
            string[] strArr = strBF.Split(',');
            if (int.Parse(strArr[0]) != 0)
            {
                ErrorMessage = strArr[1];
                return false;
            }
            else
            {
                ErrorMessage = "SUCCESS";
                return true;
            }
        }

        /// <summary>Get SCPI Command</summary>
        /// <param name="cmd">Command line</param>
        /// <returns>Command results</returns>
        public string getSCPIcommand(string cmd)
        {
            return scpi.Parse(cmd).Trim('\n').Trim('\"');
        }

    }
}
