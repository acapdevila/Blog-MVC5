using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;

namespace Blog.Modelo.Posts
{
    public class BlogEntidad 
    {
        public BlogEntidad()
        {

        }

        public int Id { get; set; }
        public string Subtitulo { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }

        public string ContenidoHtml { get; set; }
        public string Autor { get; set; }


        public string Descripcion { get; set; }
        public string PalabrasClave { get; set; }
        public string UrlImagenPrincipal { get; set; }
        

        public static BlogEntidad CrearNuevoPorDefecto()
        {
            return  new BlogEntidad();
        }
    }
}
