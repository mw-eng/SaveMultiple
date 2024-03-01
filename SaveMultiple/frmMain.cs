using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using AgilentPNA835x;

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
            Type typeFromProgID = Type.GetTypeFromProgID("AgilentPNA835x.Application");
            app = (IApplication)Activator.CreateInstance(typeFromProgID);
            scpi = app.ScpiStringParser;
        }
    }
}
