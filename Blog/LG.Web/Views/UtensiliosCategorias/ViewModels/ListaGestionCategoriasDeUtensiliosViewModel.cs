using Blog.Servicios.Utensilios.Dtos;
using PagedList;

namespace LG.Web.Views.UtensiliosCategorias.ViewModels
{
    public class ListaGestionCategoriasDeUtensiliosViewModel
    {

        public string BuscarPor { get; set; }

        public IPagedList<LineaGestionCategoriaDeUtensilio> Categorias { get; set; }
    }
}