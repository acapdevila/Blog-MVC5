namespace Blog.Modelo.Recetas
{
    public class IngrecienteReceta
    {
        public int Id { get; set; }

        public Receta Receta { get; set; }

        public Ingrediente Ingrediente { get; set; }
    }
}
