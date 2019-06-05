using System.Collections.Generic;
using Ac.Modelo.Posts;
using Ac.ViewModels.Sidebar;
using PagedList;

namespace Ac.ViewModels.Blog
{
    public class ArchivoViewModel
    {
        public ArchivoItemViewModel ArchivoItem { get; set; }
        public List<LineaResumenPost> ListaPosts { get; set; }
        public string Titulo {
            get { return string.Format("{0} {1}", ArchivoItem.NombreMes, ArchivoItem.Anyo); } 
        }
    }
}
