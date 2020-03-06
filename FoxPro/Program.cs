using System;
using System.Data.OleDb;

namespace FoxPro
{
    class Program
    {
        static void Main(string[] args)
        {
            OleDbConnection con = new OleDbConnection("Data Source=D:\\MILENIUM\\SERS2019\\siaf.dbc");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from invcli";
            OleDbDataReader Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                var a = $"{Reader[0]}";
            }
            Reader.Close();
            con.Close(); con.Dispose();
            Console.ReadLine();
        }
    }
}
