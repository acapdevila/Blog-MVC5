using Blog.Modelo.Posts;
using PagedList;

namespace Blog.ViewModels.Blog
{
    public class ListaPostsBlogResumidosViewModel
    {
        public ListaPostsBlogResumidosViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
