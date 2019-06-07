using Blog.Modelo.Utensilios;
using Blog.Smoothies.Views.UtensiliosGestion.ViewModels.Editores;

namespace Blog.Smoothies.Views.UtensiliosGestion.ViewModels
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
