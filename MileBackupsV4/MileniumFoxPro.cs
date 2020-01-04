using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MileBackupsV4
{
    class MileniumFoxPro
    {
        public void Respaldar()
        {// Seteamos
            int carpetas = 0;
            int Carchivos = 0;

            // Seteamos la ruta
            string rutaDestino = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Respaldos Milenium\\{DateTime.Now:yyyy.MM.dd-HH.mm.ss}";

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

            // Obtenemos las carpetas del año anterior
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

            // Recorredor
            int iteracionCarpeta = 0;

            // Recorremos las carpetas
            foreach (var carpetaCurso in listFolder)
            {
                // Sumamos la iteración
                iteracionCarpeta = iteracionCarpeta + 1;
                carpetas = carpetas + 1;

                // Subcarpeta
                var subCarpeta = Directory.GetFiles(carpetaCurso).ToList();

                // Sumamos la iteracion del archivo
                int iteracionArchivo = 0;

                // listamos los archivos
                foreach (var row in subCarpeta)
                {
                    // Sumamos la iteracion
                    iteracionArchivo = iteracionArchivo + 1;
                    Carchivos = Carchivos + 1;

                    // Fileinfo
                    FileInfo fileInfo = new FileInfo(row);

                    // Mostramos mensaje
                    Console.WriteLine($"Copiando archivo {iteracionArchivo:n0} de {subCarpeta.Count:n0} - Copiando carpeta {iteracionCarpeta:n0} de {listFolder.Count:n0} - {fileInfo.FullName}");

                    // Carpeta actual
                    string carpeta = $"{carpetaCurso.Replace($"{rutaOrigen}\\", "")}";

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

            Console.WriteLine($"Se han copiado {carpetas:n0} carpetas y {Carchivos:n0} archivos");
            Console.WriteLine($"{rutaOrigen}");
            Console.WriteLine("Proceso terminado");
        }
    }
}
