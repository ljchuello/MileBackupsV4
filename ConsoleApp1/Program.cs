using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf("https://www.google.com/", PdfSharp.PageSize.A4);
                pdf.Save(@"D:\wwwroot\asd.pdf");
                res = ms.ToArray();
            }
        }
    }
}
