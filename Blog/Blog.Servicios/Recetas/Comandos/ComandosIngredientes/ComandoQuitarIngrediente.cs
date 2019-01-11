namespace Blog.Servicios.Recetas.Comandos.ComandosIngredientes
{
    public class ComandoQuitarIngrediente
    {
        public ComandoQuitarIngrediente()
        {
            
        }

        public ComandoQuitarIngrediente(int posicion)
        {
            Posicion = posicion;
        }
        public int Posicion { get; set; }
    }
}
