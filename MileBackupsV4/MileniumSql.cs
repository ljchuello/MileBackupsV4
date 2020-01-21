using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace MileBackupsV4
{
    public class MileniumSql
    {
        public string Id { set; get; } = string.Empty;
        public string Servidor { set; get; } = string.Empty;
        public string Instancia { set; get; } = string.Empty;

        public void Respaldar()
        {
            // Validando directorio
            if (!Directory.Exists($"{Configuracion.Ruta}\\Sql"))
            {
                // Creamos
                Directory.CreateDirectory($"{Configuracion.Ruta}\\Sql");
            }

            // Preguntamos
            Console.WriteLine("\nDesea respaldar la base de datos de la web (SQL SERVER)?");
            Console.WriteLine("Presione la letra \"S\" para realizar respaldo ó cualquier letra para no respaldar");
            if (Console.ReadLine()?.ToLower() != "s")
            {
                Console.WriteLine("Omitido por el usuario");
                return;
            }

            // Almacen
            List<MileniumSql> list = new List<MileniumSql>();

            // Listamos las instancias
            Console.WriteLine("Listando instancias...");
            Console.WriteLine("Por favor espere...");
            var dataTable = SqlDataSourceEnumerator.Instance.GetDataSources();

            // Iteracion
            int iteracion = 0;

            // Depuramos las instancias
            foreach (DataRow row in dataTable.Rows)
            {
                if (row[0].ToString().ToLower() == Environment.MachineName.ToLower())
                {
                    iteracion = iteracion + 1;
                    MileniumSql mileniumSql = new MileniumSql();
                    mileniumSql.Id = $"{iteracion}";
                    mileniumSql.Servidor = $"{row[0]}";
                    mileniumSql.Instancia = $"{row[1]}";
                    list.Add(mileniumSql);
                }
            }

            // Si no hay instancias lanzamos error
            if (list.Count == 0)
            {
                Console.WriteLine("No se encuentra ó está caida la base de datos");
                Console.WriteLine("No se respaldará la base de datos SQL");
                return;
            }

            // Seleccione la opcion que corresponda
            foreach (var row in list)
            {
                Console.WriteLine(string.IsNullOrEmpty(row.Instancia)
                    ? $"{row.Id}\t{row.Servidor}"
                    : $"{row.Id}\t{row.Servidor}\\{row.Instancia}");
            }
            Console.WriteLine("\nSeleccione una opción a respaldar");
            var id = Console.ReadLine();

            // Validamos
            if (!list.Exists(x => x.Id.ToLower() == id.ToLower()))
            {
                Console.WriteLine("El ID no existe");
                Console.WriteLine("No se respaldará la base de datos SQL");
                return;
            }

            // Obtenemos
            MileniumSql sql = list.FirstOrDefault(x => x.Id.ToLower() == id.ToLower());

            // Establecemos la conexión
            SqlConnection SqlConnection = new SqlConnection();
            if (!string.IsNullOrWhiteSpace(sql.Instancia))
            {
                SqlConnection = new SqlConnection($"data source={sql.Servidor}\\{sql.Instancia}; initial catalog=MileniumFact; persist security info=True; user id=sa; password=Sermatick3000; MultipleActiveResultSets=True;Connection Timeout=15;");
            }
            else
            {
                SqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog=MileniumFact; persist security info=True; user id=sa; password=Sermatick3000; MultipleActiveResultSets=True;Connection Timeout=15;");
            }

            // -
            Console.WriteLine("Intentamos abrir la conexión");
            SqlConnection.Open();

            string temp = $"{Configuracion.Ruta}\\Sql\\";

            // -
            Console.WriteLine("Respaldando... Espere...");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "\nDECLARE @name VARCHAR(50)" +
                                     "\nDECLARE @path VARCHAR(256)" +
                                     "\nDECLARE @fileName VARCHAR(256)" +
                                     "\nDECLARE @fileDate VARCHAR(20)" +
                                     $"\nSET @path = '{temp}'" +
                                     "\nSELECT @fileDate = REPLACE(CONVERT(VARCHAR(20), GETDATE(), 120), ':', '-');" +
                                     "\nDECLARE db_cursor CURSOR FOR" +
                                     "\nSELECT name" +
                                     "\nFROM master.dbo.sysdatabases" +
                                     "\nWHERE name IN('MileniumFact')" +
                                     "\nOPEN db_cursor" +
                                     "\nFETCH NEXT FROM db_cursor INTO @name" +
                                     "\nWHILE @@FETCH_STATUS = 0" +
                                     "\nBEGIN" +
                                     "\nSET @fileName = @path + @name + '_' + @fileDate + '.BAK'" +
                                     "\nBACKUP DATABASE @name TO DISK = @fileName" +
                                     "\nFETCH NEXT FROM db_cursor INTO @name" +
                                     "\nEND" +
                                     "\nCLOSE db_cursor" +
                                     "\nDEALLOCATE db_cursor";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();

            Console.WriteLine($"\nSe ha respaldado con éxito la base de datos SQL");
            Console.WriteLine("Presione cualquier tecla para continuar\n");
            Console.ReadLine();
        }
    }
}
