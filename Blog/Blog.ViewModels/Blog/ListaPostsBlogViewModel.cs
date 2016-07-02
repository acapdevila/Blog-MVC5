using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Blog
{
    public class ListaPostsBlogViewModel
    {
        public ListaPostsBlogViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
