using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public string Id { get; set; }
        public string NombreEtiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
