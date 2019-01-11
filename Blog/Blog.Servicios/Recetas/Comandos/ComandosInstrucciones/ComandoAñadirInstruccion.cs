namespace Blog.Servicios.Recetas.Comandos.ComandosInstrucciones
{
    public  class ComandoAñadirInstruccion
    {
        public ComandoAñadirInstruccion()
        {
            
        }

        public ComandoAñadirInstruccion(string nombre)
        {
            Nombre = nombre;
        }
        public string Nombre { get; set; }
    }
}
