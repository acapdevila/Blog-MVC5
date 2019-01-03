namespace Blog.Modelo.Recetas
{
    public  class Instruccion
    {
        protected Instruccion()
        {
            
        }

        public Instruccion(string nombre)
        {
            Nombre = nombre;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
