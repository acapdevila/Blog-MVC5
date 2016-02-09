using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;

namespace Blog.Modelo
{
    public class Post : IEntidadConTags
    {
        public static Post CrearNuevoPorDefecto()
        {
            return new Post 
            {
                Autor = "Albert Capdevila",
                EsBorrador = true,
                FechaPost = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(5)
            };
        }

        public Post()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public string Subtitulo { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }
        public bool EsBorrador { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public string TagsSeparadosPorComma => Tags.Any() ? string.Join(Tag.Separador + " ", Tags.Select(m=>m.Nombre)) : string.Empty;

   }
}
