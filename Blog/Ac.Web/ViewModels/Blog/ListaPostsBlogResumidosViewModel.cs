using Ac.Modelo.Posts;
using PagedList;

namespace Ac.ViewModels.Blog
{
    public class ListaPostsBlogResumidosViewModel
    {
        public ListaPostsBlogResumidosViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
