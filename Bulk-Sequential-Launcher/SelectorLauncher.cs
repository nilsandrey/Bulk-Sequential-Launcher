using System;
using System.IO;
using System.Windows.Forms;
using Bulk_Sequential_Launcher.Properties;
using Bulk_Sequential_Launcher.Runners;

namespace Bulk_Sequential_Launcher
{
    public partial class SelectorLauncher : Form
    {
        private bool _refreshing;

        public SelectorLauncher()
        {
            InitializeComponent();
        }

        private string CurrentPath
        {
            get
            {
                if (string.Equals(tbPath.Text, string.Empty, StringComparison.Ordinal))
                    tbPath.Text = Application.StartupPath;
                return tbPath.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            UseWaitCursor = true;
            try
            {
                for (var idx = 0; idx < checkedListBox1.Items.Count; idx++)
                {
                    var processPath = checkedListBox1.Items[idx].ToString();
                    if ((checkedListBox1.GetItemCheckState(idx) == CheckState.Checked)
                        && File.Exists(processPath))
                    {
                        SetItemInProgress(idx);
                        var exitOk = ExecuteFile(processPath);
                        SetItemFinished(idx, exitOk);
                    }
                }
                if (Resources.FinishedCaption != null)
                    Text = Resources.FinishedCaption;
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private bool ExecuteFile(string processPath)
        {
            var info = new FileInfo(processPath);
            IFileRunner runner = RunnerFactory.CreateRunnerFor(info.Extension.Substring(1));
            return runner != null && runner.ExecuteFile(processPath, txParameters.Text);
        }

        /// <summary>
        ///     Cambia el texto del elemento para indicar si terminó bien o no.
        /// </summary>
        /// <param name="index">Indica el índice del elemento que terminó su ejecución.</param>
        /// <param name="exitOk">Indica si tentativamente terminó bien o no.</param>
        private void SetItemFinished(int index, bool exitOk = true)
        {
            var prefix = exitOk ? Resources.WellFinishedPrefix : Resources.FinishedWithErrorsPrefix;
            var newLine = checkedListBox1.Items[index].ToString().Replace(Resources.InProgressPrefix, prefix);
            checkedListBox1.Items[index] = newLine;
            checkedListBox1.SetItemChecked(index, false);
        }

        private void SetItemInProgress(int idx)
        {
            checkedListBox1.Items[idx] = Resources.InProgressPrefix + checkedListBox1.Items[idx];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        /// <summary>
        /// Adds the files on the path defined by <paramref name="currentPath"/> as new items
        /// on the list <code>checkedListBox1</code>.
        /// Recursively calls recursively to itself for each subdirectory.
        /// </summary>
        /// <param name="currentPath">Path where to look for file names to execute.</param>
        private void AddFiles2List(string currentPath)
        {
            var files = Directory.EnumerateFiles(currentPath);
            foreach (var file in files)
            {
                var item = checkedListBox1.Items.Add(file);
                checkedListBox1.SetItemChecked(item, IsExtensionSupported(file));
            }
            var folders = Directory.EnumerateDirectories(currentPath);
            foreach (var folder in folders)
            {
                AddFiles2List(folder);
            }
        } 
        private void RefreshFileList()
        {
            if (!_refreshing && Directory.Exists(CurrentPath))
                try
                {
                    btnGo.Enabled = false;
                    _refreshing = true;
                    Text = Resources.InProgressPrefix;
                    checkedListBox1.Items.Clear();
                    AddFiles2List(CurrentPath);
                }
                finally
                {
                    _refreshing = false;
                    btnGo.Enabled = true;
                }
        }

        private static bool IsExtensionSupported(string file)
        {
            var extension = new FileInfo(file).Extension.Substring(1);
            return RunnerFactory.SupportedExtensions.Contains(extension);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void tbPath_Enter(object sender, EventArgs e)
        {
            tbPath.SelectAll();
        }
    }
}