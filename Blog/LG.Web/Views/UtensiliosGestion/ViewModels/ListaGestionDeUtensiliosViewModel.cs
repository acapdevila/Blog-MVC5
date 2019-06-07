using Blog.Servicios.Utensilios.Dtos;
using PagedList;

namespace LG.Web.Views.UtensiliosGestion.ViewModels
{
    public class ListaGestionDeUtensiliosViewModel
    {

        public string BuscarPor { get; set; }

        public IPagedList<LineaGestionDeUtensilio> Utensilios { get; set; }
    }
}