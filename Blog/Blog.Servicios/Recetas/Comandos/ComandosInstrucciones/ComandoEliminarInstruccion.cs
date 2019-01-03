namespace Blog.Servicios.Recetas.Comandos.ComandosInstrucciones
{
    public class ComandoEliminarInstruccion
    {
        public ComandoEliminarInstruccion()
        {
            
        }

        public ComandoEliminarInstruccion(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
