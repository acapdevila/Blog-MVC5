namespace Blog.Servicios.Recetas.Comandos.ComandosInstrucciones
{
    public  class ComandoEditarInstruccion
    {
        public ComandoEditarInstruccion()
        {
            
        }

        public ComandoEditarInstruccion(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
