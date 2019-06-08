using Ac.Dominio.Posts;
using Ac.Dominio.Tags;
using PagedList;

namespace Ac.Web.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public Tag Etiqueta { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
