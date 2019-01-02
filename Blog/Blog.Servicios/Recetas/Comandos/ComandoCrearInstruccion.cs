namespace Blog.Servicios.Recetas.Comandos
{
    public  class ComandoCrearInstruccion
    {
        public ComandoCrearInstruccion()
        {
            
        }

        public ComandoCrearInstruccion(string nombre)
        {
            Nombre = nombre;
        }
        public string Nombre { get; set; }
    }
}
