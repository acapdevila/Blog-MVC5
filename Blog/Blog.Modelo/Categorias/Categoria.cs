using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

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

    public static class ExtensionesCategoria
    {
        public static readonly char SeparadorCategorias = ';';

        public static IQueryable<Categoria> ConPostsPublicados(this IQueryable<Categoria> categorias)
        {
            return categorias.Where(m => m.Posts.Any(p => !p.EsBorrador && p.FechaPublicacion <= DateTime.Now));
        }

        public static string CategoriasSeparadasPorComma(this ICollection<Categoria> categorias)
        {
            return categorias.Any() ? string.Join(SeparadorCategorias + " ", categorias.Select(m => m.Nombre)) : string.Empty;
        }
    }
}
