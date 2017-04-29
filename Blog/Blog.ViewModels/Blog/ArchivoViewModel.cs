using System.Collections.Generic;
using Blog.Modelo.Posts;
using Blog.ViewModels.Sidebar;
using PagedList;

namespace Blog.ViewModels.Blog
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
