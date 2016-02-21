using System.Collections.Generic;
using Blog.Modelo.Posts;
using PagedList;

namespace Blog.Web.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public string NombreEtiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
