using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bulk_Sequential_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.
            var pinf = System.Diagnostics.Process.Start(
                "d:\\Storage-Software Repos\\Manual\\@Microsoft\\@Office\\MS office 2016\\Office2016-Updates\\Office 2016 x64 Updates Marzo 2016\\Office2016-x64\\Security\\ieawsdc2016-kb3085538-fullfile-x64-glb.exe");
            pinf?.WaitForExit();
            this.Text = "Finished";
        }
    }
}
