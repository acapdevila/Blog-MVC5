using Ac.Modelo.Posts;
using Ac.Modelo.Tags;
using PagedList;

namespace Ac.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public Tag Etiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
