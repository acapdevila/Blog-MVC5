using System.Collections.Generic;
using Blog.Modelo.Posts;

namespace Blog.Modelo.Categorias
{
    public class Categoria
    {
        public Categoria()
        {
            Posts = new List<Post>();
        }
        
        public int Id { get; set; }

        public int BlogId { get; set; }
        public string Nombre { get; set; }
        public string UrlSlug { get; set; }

        public BlogEntidad Blog { get; set; }

        public ICollection<Post> Posts { get; set; }

        public static Categoria CrearNuevaPorDefecto(int blogId)
        {
            return new Categoria
            {
                BlogId = blogId
            };
        }

        public void CambiarNombre(string nombre)
        {
            Nombre = Nombre;
        }

        public void CambiarUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }
    }
}
