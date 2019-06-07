using Blog.Modelo.Utensilios;
using Blog.Smoothies.Views.UtensiliosCategorias.ViewModels.Editores;

namespace Blog.Smoothies.Views.UtensiliosCategorias.ViewModels
{
    public  class EditarCategoriaUtensilioViewModel
    {
        public EditarCategoriaUtensilioViewModel() : this(new UtensilioCategoria())
        {
            
        }

        public EditarCategoriaUtensilioViewModel(UtensilioCategoria categoria)
        {
            EditorCategoria = new EditorCategoriaDeUtensilio(categoria);
        }

        public EditorCategoriaDeUtensilio EditorCategoria { get; set; }
        
    }
}
