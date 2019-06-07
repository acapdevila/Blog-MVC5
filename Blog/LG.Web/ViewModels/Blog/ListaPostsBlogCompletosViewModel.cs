using Blog.Modelo.Posts;
using PagedList;

namespace LG.Web.ViewModels.Blog
{
    public class ListaPostsBlogCompletosViewModel
    {
        public ListaPostsBlogCompletosViewModel()
        {
            
        }
        public IPagedList<LineaPostCompleto> ListaPosts { get; set; }

    }

 
}
