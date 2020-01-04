using System;
using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace MileBackupsV4
{
    public class MileniumWeb
    {
        public int Id { set; get; } = 0;
        public string Descripcion { set; get; } = string.Empty;
        public string Path { set; get; } = string.Empty;

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
                    mileniumWeb.Id = (int)site.Id;

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
