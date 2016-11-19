using System.Collections.Generic;

namespace Bulk_Sequential_Launcher.Runners
{
    interface IFileRunner
    {
        /// <summary>
        /// Determina las extensiones soportadas por el runner.
        /// </summary>
        List<string> SupportedExtensions { get; }

        /// <summary>
        /// Ejecutar el archivo pasado en <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Archivo a ejecutar. Se espera el amino completo 
        ///     incluido el nombre del archivo.</param>
        /// <param name="parameters"></param>
        /// <returns><code>true</code> si el archivo ejecutó satisfactoriamente.
        /// <code>false</code> en caso contrario.</returns>
        bool ExecuteFile(string filePath, string parameters);
    }
}
