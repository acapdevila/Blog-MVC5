using System;
using System.Collections.Generic;

namespace Blog.Modelo
{
    public class Post
    {
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
    }
}
