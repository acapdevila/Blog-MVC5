using LG.Web.Views.UtensiliosCategorias.ViewModels.Editores;

namespace LG.Web.Views.UtensiliosCategorias.ViewModels
{
    public  class CrearCategoriaUtensilioViewModel
    {
        public CrearCategoriaUtensilioViewModel()
        {
            EditorCategoria = new EditorCategoriaDeUtensilio();
        }

        public CrearCategoriaUtensilioViewModel(int posicion)
        {
            EditorCategoria = new EditorCategoriaDeUtensilio(posicion);
        }

        public EditorCategoriaDeUtensilio EditorCategoria { get; set; }
    }
}
