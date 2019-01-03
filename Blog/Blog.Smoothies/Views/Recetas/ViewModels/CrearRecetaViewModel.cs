using System;
using Blog.Modelo.Recetas;
using Blog.Smoothies.Views.Recetas.ViewModels.Editores;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class CrearRecetaViewModel
    {
        public CrearRecetaViewModel() 
        {

        }

        public CrearRecetaViewModel(Receta receta) 
        {
            EditorReceta = new EditorReceta(receta);
        }
        public EditorReceta EditorReceta { get; set; }
    }
}
