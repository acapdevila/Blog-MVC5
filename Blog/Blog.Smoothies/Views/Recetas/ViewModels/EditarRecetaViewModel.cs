using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class EditarRecetaViewModel
    {
        public EditarRecetaViewModel() : this(new Receta())
        {
            
        }

        public EditarRecetaViewModel(Receta receta)
        {
            EditorReceta = new EditorRecetaPartialModel(receta);
        }

        public EditorRecetaPartialModel EditorReceta { get; set; }

    }
}
