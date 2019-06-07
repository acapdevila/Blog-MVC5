using Blog.Modelo.Posts;
using PagedList;

namespace LG.Web.ViewModels.Blog
{
    public class ResultadoBusquedaViewModel
    {

        public IPagedList<LineaPostCompleto> ListaPosts { get; set; }
        public string BuscarPor { get; set; }
    }
}
