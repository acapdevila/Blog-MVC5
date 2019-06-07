using Blog.Modelo.Posts;
using PagedList;

namespace LG.Web.ViewModels.Blog
{
    public class ListaPostsBlogResumidosViewModel
    {
        public ListaPostsBlogResumidosViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
