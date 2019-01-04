namespace Blog.Servicios.Recetas.Comandos.ComandosInstrucciones
{
    public class ComandoEliminarInstruccion
    {
        public ComandoEliminarInstruccion()
        {
            
        }

        public ComandoEliminarInstruccion(int id)
        {
            Posicion = id;
        }
        public int Posicion { get; set; }
    }
}
