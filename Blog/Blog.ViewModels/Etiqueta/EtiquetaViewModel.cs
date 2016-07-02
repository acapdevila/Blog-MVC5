using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public string NombreEtiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
