using Blog.Smoothies.Views.UtensiliosCategorias.ViewModels.Editores;

namespace Blog.Smoothies.Views.UtensiliosCategorias.ViewModels
{
    public  class CrearCategoriaUtensilioViewModel
    {
        public CrearCategoriaUtensilioViewModel()
        {
            EditorCategoria = new EditorCategoriaDeUtensilio();
        }

        public EditorCategoriaDeUtensilio EditorCategoria { get; set; }
    }
}
