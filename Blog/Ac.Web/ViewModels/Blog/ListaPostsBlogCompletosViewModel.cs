using Ac.Modelo.Posts;
using PagedList;

namespace Ac.Web.ViewModels.Blog
{
    public class ListaPostsBlogCompletosViewModel
    {
        public ListaPostsBlogCompletosViewModel()
        {
            
        }
        public IPagedList<LineaPostCompleto> ListaPosts { get; set; }

    }

 
}
