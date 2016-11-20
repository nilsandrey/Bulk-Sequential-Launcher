using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bulk_Sequential_Launcher.Runners
{
    class ExeRunner : IFileRunner
    {
        public static List<string> SupportedExtensions => new List<string> {"exe", "msu", "msi"};

        List<string> IFileRunner.SupportedExtensions => SupportedExtensions;

        public virtual bool ExecuteFile(string filePath, string parameters)
        {
            try
            {
                var process = Process.Start(filePath, parameters);
                while ((process != null) && !process.HasExited)
                    process.WaitForExit(100);
                return (process == null) || (process.ExitCode == 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
