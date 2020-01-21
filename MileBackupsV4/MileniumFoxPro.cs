using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MileBackupsV4
{
    class MileniumFoxPro
    {
        public void Respaldar()
        {
            int carpetaIteracion = 0;
            int carpetaProcesada = 0;

            int archivoIteracion = 0;
            int archivoProcesado = 0;

            Console.WriteLine("Desea realizar respaldos del sistema Milenium Contable/Milenium?");
            Console.WriteLine("Presione la letra \"S\" para realizar respaldo ó cualquier letra para no respaldar");
            if (Console.ReadLine()?.ToLower() != "s")
            {
                Console.WriteLine("Omitido por el usuario");
                return;
            }

            // Seteamos la ruta
            string rutaDestino = $"{Configuracion.Ruta}\\Milenium\\{DateTime.Now:yyyy.MM.dd-HH.mm.ss}";

            // Obtenemos la lista de las carpeta de la empresa
            //string rutaOrigen = "\\\\server-fc\\D\\Milenium";
            string rutaOrigen = $"{Environment.CurrentDirectory}";

            // Creamos el contenedor
            List<string> listFolder = new List<string>();

            // Obtenemos las carpetas del año actual
            foreach (var row in Directory.GetDirectories(rutaOrigen, $"*{DateTime.Now:yy}").ToList())
            {
                // Añadimos
                listFolder.Add(row);
            }

            // Obtenemos las carpetas del año -1
            foreach (var row in Directory.GetDirectories(rutaOrigen, $"*{DateTime.Now.AddYears(-1):yy}").ToList())
            {
                // Añadimos
                listFolder.Add(row);
            }

            // Validamos si encontramos los logos
            if (Directory.Exists($"{rutaOrigen}\\bmp"))
            {
                listFolder.Add($"{rutaOrigen}\\bmp");
            }

            // Validamos si encontramos el tmp / rep
            if (Directory.Exists($"{rutaOrigen}\\tmp\\rep"))
            {
                listFolder.Add($"{rutaOrigen}\\tmp\\rep");
            }

            // Validamos si encontramos el dbf
            if (Directory.Exists($"{rutaOrigen}\\dbf"))
            {
                listFolder.Add($"{rutaOrigen}\\dbf");
            }

            // Recorremos las carpetas
            foreach (var carpetaActual in listFolder)
            {
                // Sumamos
                carpetaIteracion = carpetaIteracion + 1;
                carpetaProcesada = carpetaProcesada + 1;

                // Subcarpeta
                List<string> contenido = Directory.GetFiles(carpetaActual).ToList();

                // Seteamos
                archivoIteracion = 0;

                // Recorremos
                foreach (var file in contenido)
                {
                    // Seteamos
                    archivoIteracion = archivoIteracion + 1;
                    archivoProcesado = archivoProcesado + 1;

                    // Fileinfo
                    FileInfo fileInfo = new FileInfo(file);

                    // Mostramos mensaje
                    Console.WriteLine($"Copiando archivo {archivoIteracion:n0} de {contenido.Count:n0} - Copiando carpeta {carpetaIteracion:n0} de {listFolder.Count:n0} - {fileInfo.FullName}");

                    // Carpeta actual
                    string carpeta = $"{carpetaActual.Replace($"{rutaOrigen}\\", "")}";

                    // Validamos que exista la carpeta
                    if (!Directory.Exists($"{rutaDestino}\\{carpeta}"))
                    {
                        Directory.CreateDirectory($"{rutaDestino}\\{carpeta}");
                    }

                    // Si no existe el archivo lo copiamos
                    if (!File.Exists($"{rutaDestino}\\{carpeta}\\{fileInfo.Name}"))
                    {
                        File.Copy(fileInfo.FullName, $"{rutaDestino}\\{carpeta}\\{fileInfo.Name}");
                    }
                }
            }

            Console.WriteLine($"\nSe han copiado las carpetas del año {DateTime.Now:yyyy} y del año {DateTime.Now.AddYears(-1):yyyy}");
            Console.WriteLine($"Un total de {carpetaProcesada:n0} carpetas y {archivoProcesado:n0} archivos...");
            Console.WriteLine("Presione cualquier tecla para continuar\n");
            Console.ReadLine();
        }
    }
}
