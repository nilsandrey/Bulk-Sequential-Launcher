using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bulk_Sequential_Launcher.Properties;

namespace Bulk_Sequential_Launcher
{
    public partial class SelectorLauncher : Form
    {
        private bool _refreshing;

        public SelectorLauncher()
        {
            InitializeComponent();
        }

        public string CurrentPath
        {
            get
            {
                if (string.Equals(tbPath.Text, string.Empty, StringComparison.Ordinal))
                    tbPath.Text = Application.StartupPath;
                return tbPath.Text;
            }
            set { tbPath.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems)
            {
                var processPath = item.ToString();
                if (File.Exists(processPath))
                {
                    var process = Process.Start(processPath, txParameters.Text);
                    while (process != null && !process.HasExited)
                        process?.WaitForExit(100);
                }
            }
            if (Resources.FinishedCaption != null)
                Text = Resources.FinishedCaption;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void RefreshFileList()
        {
            if (!_refreshing && Directory.Exists(CurrentPath))
                try
                {
                    _refreshing = true;
                    checkedListBox1.Items.Clear();
                    var files = Directory.EnumerateFiles(CurrentPath);
                    foreach (var file in files)
                    {
                        var item = checkedListBox1.Items.Add(file);
                        checkedListBox1.SetItemChecked(item, file.EndsWith("exe"));
                    }
                }
                finally
                {
                    _refreshing = false;
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetAllState(CheckState.Unchecked);
        }

        private void SetAllState(CheckState checkState)
        {
            for (var idx = 0; idx < checkedListBox1.Items.Count; idx++)
                checkedListBox1.SetItemCheckState(idx, checkState);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetAllState(CheckState.Checked);
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            RefreshFileList();
        }
    }
}