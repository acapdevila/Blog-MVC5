namespace Blog.Servicios.Recetas.Comandos.ComandosIngredientes
{
    public  class ComandoEditarIngrediente
    {
        public ComandoEditarIngrediente()
        {
            
        }

        public ComandoEditarIngrediente(int posicion, string nombre)
        {
            Posicion = posicion;
            Nombre = nombre;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }
    }
}
