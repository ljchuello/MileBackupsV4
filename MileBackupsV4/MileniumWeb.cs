using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Web.Administration;

namespace MileBackupsV4
{
    public class MileniumWeb
    {
        public string Id { set; get; } = string.Empty;
        public string Descripcion { set; get; } = string.Empty;
        public string Path { set; get; } = string.Empty;

        public void Respaldar()
        {
            // Iteracion
            int iteracion = 0;

            // Preguntamos
            Console.WriteLine("\nDesea respaldar sitio web (RIDE y XML)?");
            Console.WriteLine("Presione la letra \"S\" para realizar respaldo ó cualquier letra para no respaldar");
            if (Console.ReadLine()?.ToLower() != "s")
            {
                Console.WriteLine("Omitido por el usuario");
                return;
            }

            // Obtenemos los sitios
            List<MileniumWeb> sitios = ListarSitios();

            // Listamos los sitios
            Console.WriteLine("\nID\tSitio");
            foreach (var row in sitios)
            {
                Console.WriteLine($"{row.Id}\t{row.Descripcion}");
            }

            // 
            string idSitio;

            // Automatizamos el sitio
            if (sitios.Count == 1)
            {
                // Un sólo sitio
                idSitio = sitios[0].Id;
            }
            else
            {
                // Diferente de 01 sitios
                Console.WriteLine("\nIngrese el ID del sitio que desea respaldar");
                idSitio = Console.ReadLine();
            }


            // Validamos que sea diferente de S
            if (!sitios.Exists(x => x.Id == idSitio))
            {
                Console.WriteLine("No existe el Id del sitio, NO SE RESPALDARÁ. Presione cualquier tecla para continuar");
                Console.ReadLine();
                return;
            }

            // Obtenemos el sitio seleccionado
            MileniumWeb mileniumWeb = sitios.FirstOrDefault(x => x.Id == idSitio);

            // Validamos si existe la carpeta de los xml autorizados
            Console.WriteLine("\nValidando que se pueda copiar los XML autorizados");
            if (Directory.Exists($"{mileniumWeb?.Path}\\Archivos\\XML\\05-RESPUESTA_AUTORIZACION_AUTORIZADO"))
            {
                // Seteamos
                iteracion = 0;

                // Obtenemos la lista
                List<string> xmlAutorizados = Directory.GetFiles($"{mileniumWeb?.Path}\\Archivos\\XML\\05-RESPUESTA_AUTORIZACION_AUTORIZADO", "*.xml").ToList();

                // Recorremos y copiamos
                foreach (var row in xmlAutorizados)
                {
                    // Obtenemos los datos del origen
                    FileInfo fileInfo = new FileInfo(row);

                    // Valimos que exista el destino
                    Configuracion.RutaCrear(Configuracion.WebXmlAutorizados);

                    // Mostramos
                    iteracion = iteracion + 1;
                    Console.WriteLine($"Copiando XML (AUTORIZADO) {iteracion:n0} de {xmlAutorizados.Count:n0}");

                    // Validamos que no exista el archivo
                    if (!File.Exists($"{Configuracion.WebXmlAutorizados}\\{fileInfo.Name}"))
                    {
                        File.Copy($"{fileInfo.FullName}", $"{Configuracion.WebXmlAutorizados}\\{fileInfo.Name}");
                    }
                }
            }

            Console.WriteLine("Se han copiado todos los XML (AUTORIZADOS)... Espere 3 Seg.");
            Thread.Sleep(2500);

            // Validamos si existe la carpeta de los RIDE
            Console.WriteLine("\nValidando que se pueda copiar los RIDE");
            if (Directory.Exists($"{mileniumWeb?.Path}\\Archivos\\PDF"))
            {
                // Seteamos
                iteracion = 0;

                // Obtenemos la lista
                List<string> list = Directory.GetFiles($"{mileniumWeb?.Path}\\Archivos\\PDF", "*.pdf").ToList();

                // Recorremos y copiamos
                foreach (var row in list)
                {
                    // Obtenemos los datos del origen
                    FileInfo fileInfo = new FileInfo(row);

                    // Valimos que exista el destino
                    Configuracion.RutaCrear(Configuracion.WebPdf);

                    // Mostramos
                    iteracion = iteracion + 1;
                    Console.WriteLine($"Copiando RIDE {iteracion:n0} de {list.Count:n0}");

                    // Validamos que no exista el archivo
                    if (!File.Exists($"{Configuracion.WebPdf}\\{fileInfo.Name}"))
                    {
                        File.Copy($"{fileInfo.FullName}", $"{Configuracion.WebPdf}\\{fileInfo.Name}");
                    }
                }
            }

            Console.WriteLine("Se han copiado todos los RIDE... Espere 3 Seg.");
            Thread.Sleep(2500);

            // Validamos si existe la carpeta de los RIDE
            Console.WriteLine("\nValidando que se pueda copiar los logos");
            if (Directory.Exists($"{mileniumWeb?.Path}\\Archivos\\Empresas\\Logotipos"))
            {
                // Seteamos
                iteracion = 0;

                // Obtenemos la lista
                List<string> list = Directory.GetFiles($"{mileniumWeb?.Path}\\Archivos\\Empresas\\Logotipos").ToList();

                // Recorremos y copiamos
                foreach (var row in list)
                {
                    // Obtenemos los datos del origen
                    FileInfo fileInfo = new FileInfo(row);

                    // Valimos que exista el destino
                    Configuracion.RutaCrear(Configuracion.WebLogotipo);

                    // Mostramos
                    iteracion = iteracion + 1;
                    Console.WriteLine($"Copiando logotipo {iteracion:n0} de {list.Count:n0}");

                    // Validamos que no exista el archivo
                    if (!File.Exists($"{Configuracion.WebLogotipo}\\{fileInfo.Name}"))
                    {
                        File.Copy($"{fileInfo.FullName}", $"{Configuracion.WebLogotipo}\\{fileInfo.Name}");
                    }
                }
            }

            Console.WriteLine("Se han copiado todos los logotipo... Espere 3 Seg.");
            Thread.Sleep(2500);

            // Validamos si existe la carpeta de las firmas
            Console.WriteLine("\nValidando que se pueda copiar las firmas");
            if (Directory.Exists($"{mileniumWeb?.Path}\\Archivos\\Empresas\\FirmasElectronicas"))
            {
                // Seteamos
                iteracion = 0;

                // Obtenemos la lista
                List<string> list = Directory.GetFiles($"{mileniumWeb?.Path}\\Archivos\\Empresas\\FirmasElectronicas", "*.p12").ToList();

                // Recorremos y copiamos
                foreach (var row in list)
                {
                    // Obtenemos los datos del origen
                    FileInfo fileInfo = new FileInfo(row);

                    // Valimos que exista el destino
                    Configuracion.RutaCrear(Configuracion.WebFirmasElectronicas);

                    // Mostramos
                    iteracion = iteracion + 1;
                    Console.WriteLine($"Copiando firma {iteracion:n0} de {list.Count:n0}");

                    // Validamos que no exista el archivo
                    if (!File.Exists($"{Configuracion.WebFirmasElectronicas}\\{fileInfo.Name}"))
                    {
                        File.Copy($"{fileInfo.FullName}", $"{Configuracion.WebFirmasElectronicas}\\{fileInfo.Name}");
                    }
                }
            }

            Console.WriteLine("Se han copiado todas las firmas");

            Console.WriteLine("\nSe han copiado los XML (AUTORIZADOS), RIDES, Logotipos y firmas electrónicas");
            Console.WriteLine("Presione cualquier tecla para continuar");
            Console.ReadLine();
        }

        /// <summary>
        /// Devuelve lista con todos los sitios
        /// </summary>
        /// <returns></returns>
        public List<MileniumWeb> ListarSitios()
        {
            List <MileniumWeb> list = new List<MileniumWeb>();

            using (ServerManager serverManager = ServerManager.OpenRemote("localhost"))
            {
                MileniumWeb mileniumWeb = new MileniumWeb();
                foreach (Site site in serverManager.Sites)
                {
                    // id
                    mileniumWeb.Id = $"{site.Id:n0}";

                    // Name
                    mileniumWeb.Descripcion = site.Name;

                    foreach (Application application in site.Applications)
                    {
                        foreach (VirtualDirectory virtualDirectory in application.VirtualDirectories)
                        {
                            // Path
                            mileniumWeb.Path = virtualDirectory.PhysicalPath;
                        }
                    }
                }

                // Agregamos
                list.Add(mileniumWeb);
            }

            return list;
        }
    }
}