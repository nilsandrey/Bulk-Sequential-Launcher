using System.Collections.Generic;

namespace Bulk_Sequential_Launcher.Runners
{
    internal static class RunnerFactory
    {
        private static List<string> _supportedExtensions;

        public static IFileRunner CreateRunnerFor(string extension)
        { 
            if (ExeRunner.SupportedExtensions.Contains(extension))
                return new ExeRunner();
            // Nothing so far? well, then null.
            return new ExeRunner();
        }

        public static List<string> SupportedExtensions
        {
            get
            {
                if (_supportedExtensions != null) { return _supportedExtensions;}
                _supportedExtensions = new List<string>();
                if (ExeRunner.SupportedExtensions != null)
                    _supportedExtensions.AddRange(ExeRunner.SupportedExtensions);
                return _supportedExtensions;
            }
        }
    }
}