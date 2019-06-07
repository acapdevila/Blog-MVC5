using Blog.Servicios.Recetas.Dtos;
using PagedList;

namespace LG.Web.Views.Recetas.ViewModels
{
    public class ListaGestionRecetasViewModel
    {

        public string BuscarPor { get; set; }

        public IPagedList<LineaGestionReceta> Recetas { get; set; }
    }
}