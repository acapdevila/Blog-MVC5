using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;

namespace Blog.Modelo.Posts
{
    public class Post : IEntidadConTags
    {
        public static Post CrearNuevoPorDefecto(string autor, int blogId)
        {
            return new Post
            {
                BlogId = blogId,
                Autor = autor,
                EsBorrador = true,
                EsRssAtom = false,
                FechaPost = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(5)
            };
        }

        public Post()
        {
            Tags = new List<Tag>();
            Categorias = new List<Categoria>();
        }

        public int Id { get; set; }
        public int BlogId { get; set; }
        
        public string Subtitulo { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string UrlImagenPrincipal { get; set; }

        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }

        public string PostContenidoHtml { get; set; }
        public bool EsBorrador { get; set; }
        public bool EsRssAtom { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string Autor { get; set; }
        
        public string DatosEstructurados { get; set; }

        public string PalabrasClave
        {
            get
            {
                if (!Tags.Any()) return null;
                return string.Join(",", Tags);
            }
        }

        public BlogEntidad Blog { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public ICollection<Categoria> Categorias { get; set; }

        public bool EsPublico
        {
            get { return !EsBorrador && FechaPublicacion <= DateTime.Now;}
        }
    
     }
}
