using System;
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
                //new MileniumFoxPro().Respaldar();

                // Web
                //new MileniumWeb().Respaldar();

                // SQL
                new MileniumSql().a();
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
