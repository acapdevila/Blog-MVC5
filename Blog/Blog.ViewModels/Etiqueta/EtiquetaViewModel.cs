using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using PagedList;

namespace Blog.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public Tag Etiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
