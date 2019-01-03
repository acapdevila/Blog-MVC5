namespace Blog.Servicios.Recetas.Comandos.ComandosIngredientes
{
    public class ComandoQuitarIngrediente
    {
        public ComandoQuitarIngrediente()
        {
            
        }

        public ComandoQuitarIngrediente(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
