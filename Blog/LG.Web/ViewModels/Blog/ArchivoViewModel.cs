using System.Collections.Generic;
using Blog.Modelo.Posts;
using LG.Web.ViewModels.Sidebar;

namespace LG.Web.ViewModels.Blog
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
