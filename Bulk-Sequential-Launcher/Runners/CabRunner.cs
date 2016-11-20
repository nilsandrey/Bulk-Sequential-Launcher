using System;
using System.Collections.Generic;
using static System.String;

namespace Bulk_Sequential_Launcher.Runners
{
    internal class CabRunner : ExeRunner, IFileRunner
    {
        public new static List<string> SupportedExtensions => new List<string> {"cab"};

        List<string> IFileRunner.SupportedExtensions => SupportedExtensions;

        /// <summary>
        ///     Ejecutar el archivo pasado en <paramref name="filePath" />.
        /// </summary>
        /// <param name="filePath">
        ///     Archivo a ejecutar. Se espera el amino completo
        ///     incluido el nombre del archivo.
        /// </param>
        /// <param name="parameters"></param>
        /// <returns>
        ///     <code>true</code> si el archivo ejecutó satisfactoriamente.
        ///     <code>false</code> en caso contrario.
        /// </returns>
        public override bool ExecuteFile(string filePath, string parameters)
        {
            try
            {
                var commandLine = "dism.exe /online /add-package /PackagePath:\"{0}\" {1}";
                commandLine = Format(commandLine, filePath, parameters);
                return base.ExecuteFile(commandLine, parameters);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}