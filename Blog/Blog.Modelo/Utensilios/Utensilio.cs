using Blog.Modelo.Imagenes;

namespace Blog.Modelo.Utensilios
{
    public class Utensilio 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public Imagen Imagen { get; set; }

        public string Link { get; set; }

        public UtensilioCategoria Categoria { get; set; }

    }
}