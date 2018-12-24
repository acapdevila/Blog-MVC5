using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class EliminarRecetaViewModel
    {
        public EliminarRecetaViewModel()
        {
            
        }

        public EliminarRecetaViewModel(Receta receta) : this()
        {
            Id = receta.Id;
            Nombre = receta.Nombre;
            CategoriaReceta = receta.CategoriaReceta;
            Descripcion = receta.Descripcion;
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string CategoriaReceta { get; set; }
    }
}
