using LG.Web.Views.UtensiliosGestion.ViewModels.Editores;

namespace LG.Web.Views.UtensiliosGestion.ViewModels
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
