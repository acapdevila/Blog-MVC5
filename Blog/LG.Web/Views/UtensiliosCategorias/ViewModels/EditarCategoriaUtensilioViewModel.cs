using Blog.Modelo.Utensilios;
using LG.Web.Views.UtensiliosCategorias.ViewModels.Editores;

namespace LG.Web.Views.UtensiliosCategorias.ViewModels
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
