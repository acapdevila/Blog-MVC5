using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Categoria
{
    public class CategoriaViewModel
    {
        public Modelo.Categorias.Categoria Categoria { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
