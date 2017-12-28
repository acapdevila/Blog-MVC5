using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Categoria
{
    public class CategoriaViewModel
    {
        public string Id { get; set; }
        public string NombreCategoria { get; set; }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }
}
