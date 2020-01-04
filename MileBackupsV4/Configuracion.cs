using System;
using System.IO;

namespace MileBackupsV4
{
    public static class Configuracion
    {
        public static string Ruta = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium";

        public static string WebXmlAutorizados = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium\\Web\\XML AUTORIZADO";
        public static string WebPdf = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium\\Web\\PDF";
        public static string WebLogotipo = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium\\Web\\Logotipo";
        public static string WebFirmasElectronicas = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium\\Web\\Firma";

        /// <summary>
        /// Valida si existe un directorio
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static bool RutaExiste(string ruta)
        {
            if (Directory.Exists(ruta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Crea un nuevo directorio
        /// </summary>
        /// <param name="ruta"></param>
        public static void RutaCrear(string ruta)
        {
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
        }
    }
}
