namespace Blog.Servicios.Recetas.Comandos.ComandosIngredientes
{
    public  class ComandoEditarIngrediente
    {
        public ComandoEditarIngrediente()
        {
            
        }

        public ComandoEditarIngrediente(int id, string nombre)
        {
            IdIngredienteReceta = id;
            Nombre = nombre;
        }
        public int IdIngredienteReceta { get; set; }
        public string Nombre { get; set; }
    }
}
