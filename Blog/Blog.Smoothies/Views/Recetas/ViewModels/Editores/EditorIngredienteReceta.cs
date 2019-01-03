using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels.Editores
{
    public class EditorIngredienteReceta
    {
        public EditorIngredienteReceta()
        {
            
        }

        public EditorIngredienteReceta(IngredienteReceta ingredienteReceta)
        {
            IdIngredienteReceta = ingredienteReceta.Id;
            NombreIngrediente = ingredienteReceta.Ingrediente.Nombre;
        }
        public int IdIngredienteReceta { get; set; }
        public string NombreIngrediente { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}