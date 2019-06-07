using Blog.Modelo.Utensilios;
using LG.Web.Views.UtensiliosGestion.ViewModels.Editores;

namespace LG.Web.Views.UtensiliosGestion.ViewModels
{
    public  class EditarUtensilioViewModel
    {
        public EditarUtensilioViewModel()
        {
            
        }

        public EditarUtensilioViewModel(Utensilio utensilio,string accionEditar, string accionSubirImagen, string accionQuitarImagen) : this()
        {
            Editor = new EditorDeUtensilio(utensilio, accionEditar, accionSubirImagen, accionQuitarImagen);
        }

        public EditorDeUtensilio Editor { get; set; }
        
    }
}
