using Blog.Modelo.Posts;
using PagedList;

namespace LG.Web.ViewModels.Categoria
{
    public class CategoriaViewModel
    {
        public global::Blog.Modelo.Categorias.Categoria Categoria { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
