using Ac.Dominio.Posts;
using PagedList;

namespace Ac.Web.ViewModels.Blog
{
    public class ListaPostsBlogResumidosViewModel
    {
        public ListaPostsBlogResumidosViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
