using Blog.Servicios.Recetas.Dtos;
using PagedList;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public class ListaGestionRecetasViewModel
    {

        public string BuscarPor { get; set; }

        public IPagedList<LineaGestionReceta> Recetas { get; set; }
    }
}