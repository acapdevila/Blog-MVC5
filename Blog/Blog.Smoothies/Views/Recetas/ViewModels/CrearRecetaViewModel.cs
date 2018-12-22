using System;
using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class CrearRecetaViewModel
    {
        public CrearRecetaViewModel() : this(new Receta())
        {

        }

        public CrearRecetaViewModel(Receta receta) 
        {
            EditorReceta = new EditorRecetaPartialModel(receta);
        }
        public EditorRecetaPartialModel EditorReceta { get; set; }
    }
}
