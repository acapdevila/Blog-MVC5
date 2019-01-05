using Blog.Modelo.Recetas;
using Blog.Smoothies.Views.Recetas.ViewModels.Editores;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class EditarRecetaViewModel
    {
        public EditarRecetaViewModel() : this(new Receta())
        {
            
        }

        public EditarRecetaViewModel(Receta receta)
        {
            EditorReceta = new EditorReceta(receta);
        }

        public EditorReceta EditorReceta { get; set; }
        
    }
}
