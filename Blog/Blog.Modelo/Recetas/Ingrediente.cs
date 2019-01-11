namespace Blog.Modelo.Recetas
{
    public  class Ingrediente
    {
        protected Ingrediente()
        {
            
        }

        public Ingrediente(string nombre)
        {
            Nombre = nombre;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
