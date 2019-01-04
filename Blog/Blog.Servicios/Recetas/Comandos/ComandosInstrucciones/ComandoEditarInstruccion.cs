namespace Blog.Servicios.Recetas.Comandos.ComandosInstrucciones
{
    public  class ComandoEditarInstruccion
    {
        public ComandoEditarInstruccion()
        {
            
        }

        public ComandoEditarInstruccion(int posicion, string nombre)
        {
            Posicion = posicion;
            Nombre = nombre;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }
    }
}
