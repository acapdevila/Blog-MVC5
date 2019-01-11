namespace Blog.Servicios.Recetas.Comandos.ComandosIngredientes
{
    public  class ComandoAñadirIngrediente
    {
        public ComandoAñadirIngrediente()
        {
            
        }

        public ComandoAñadirIngrediente(string nombre)
        {
            Nombre = nombre;
        }
        public string Nombre { get; set; }
    }
}
