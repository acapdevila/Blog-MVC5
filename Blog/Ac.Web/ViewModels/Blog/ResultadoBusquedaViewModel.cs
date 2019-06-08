using Ac.Dominio.Posts;
using PagedList;

namespace Ac.Web.ViewModels.Blog
{
    public class ResultadoBusquedaViewModel
    {

        public IPagedList<LineaPostCompleto> ListaPosts { get; set; }
        public string BuscarPor { get; set; }
    }
}
