using LG.Web.Views.Recetas.ViewModels.Editores;

namespace LG.Web.Views.Recetas.ViewModels
{
    public  class CrearRecetaViewModel
    {
        public CrearRecetaViewModel()
        {
            
        }
        public CrearRecetaViewModel(string autor) 
        {
            EditorReceta = new EditorReceta
            {
                Autor = autor,
                AccionPost = "CrearReceta",
                AccionQuitarImagen = "CrearQuitarImagen",
                AccionSubirImagen = "CrearSubirImagen"
            };
        }

        public EditorReceta EditorReceta { get; set; }
    }
}
