namespace Blog.Modelo.Recetas
{
    public class IngredienteReceta
    {
        public int Id { get; set; }

        public Receta Receta { get; set; }

        public Ingrediente Ingrediente { get; set; }

        public string Nombre => Ingrediente?.Nombre;
    }
}
