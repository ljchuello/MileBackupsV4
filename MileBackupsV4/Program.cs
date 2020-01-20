using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MileBackupsV4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Validamos si es administrador
                if (!IsAdministrator())
                {
                    Console.WriteLine("Debe ejecutar el programa con privilegios de administrador, presione una tecla para salir");
                    Console.Read();
                    Environment.Exit(0);
                }

                // Contable
                new MileniumFoxPro().Respaldar();

                // Web
                new MileniumWeb().Respaldar();

                #region Estableciendo permisos

                // Obtenemos la informacion del directorio
                DirectoryInfo directoryInfo = new DirectoryInfo(Configuracion.Ruta);

                // Obtenemos la informacion del directorio
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                // Seteamos
                directorySecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

                // Guardamos
                directoryInfo.SetAccessControl(directorySecurity);

                #endregion

                // SQL
                new MileniumSql().Respaldar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ah ocurrido un error; {ex.Message}");
                Console.ReadLine();
            }
            finally
            {
                Console.WriteLine("Trabajo terminado");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Valida si el usuario actual es administrador
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
