using Blog.Modelo.Recetas;

namespace LG.Web.Views.Recetas.ViewModels.Editores
{
    public class EditorIngredienteReceta
    {
        public EditorIngredienteReceta()
        {
            
        }

        public EditorIngredienteReceta(int posicion, IngredienteReceta ingredienteReceta)
        {
            Posicion = posicion;
            NombreIngrediente = ingredienteReceta.Nombre;
        }
        public int Posicion { get; set; }
        public string NombreIngrediente { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}