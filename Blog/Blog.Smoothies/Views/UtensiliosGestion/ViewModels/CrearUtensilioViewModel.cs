using Blog.Smoothies.Views.UtensiliosGestion.ViewModels.Editores;

namespace Blog.Smoothies.Views.UtensiliosGestion.ViewModels
{
    public  class CrearUtensilioViewModel
    {
        public CrearUtensilioViewModel()
        {
            Editor = new EditorDeUtensilio();
        }
        public CrearUtensilioViewModel(string accionCrear, string accionSubirImagen, string accionQuitarImagen)
        {
            Editor = new EditorDeUtensilio(accionCrear, accionSubirImagen, accionQuitarImagen);
        }

        public EditorDeUtensilio Editor { get; set; }
    }
}
