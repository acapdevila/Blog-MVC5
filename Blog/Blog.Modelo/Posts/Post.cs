using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blog.Modelo.Categorias;
using Blog.Modelo.Extensiones;
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
                FechaPublicacion = DateTime.Now.AddMonths(6)
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
        public string Titulo { get; private set; }

        public string Descripcion { get; set; }

        public string PalabrasClave { get; set; }

        public string UrlImagenPrincipal { get; set; }

        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }

        public string PostContenidoHtml { get; set; }
        public bool EsBorrador { get; private set; }
        public bool EsRssAtom { get; private set; }
        public DateTime FechaPublicacion { get; private set; }

        public DateTime FechaModificacion { get; set; }

        public string Autor { get; set; }
        
        public BlogEntidad Blog { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public ICollection<Categoria> Categorias { get; set; }


        public string TituloSinAcentos { get; private set; }

        public bool EsPublico
        {
            get { return !EsBorrador && FechaPublicacion <= DateTime.Now;}
        }

        public bool EsMostrarDatosEstructurados
        {
            get { return !string.IsNullOrEmpty(UrlImagenPrincipal) && !string.IsNullOrEmpty(Descripcion); }
        }

        
        public void Publicar(DateTime fechaPost, string urlSlug, bool esRssAtom)
        {
            FechaPost = fechaPost;

            UrlSlug = urlSlug.Replace(" ", "-");

            if(DateTime.Now < FechaPublicacion)
                FechaPublicacion = DateTime.Now.AddHours(-2).AddMinutes(-1);

            EsRssAtom = esRssAtom;

            EsBorrador = false;
        }

        public void ProgramarPublicacion(DateTime fechaPost, string urlSlug, bool esRssAtom, DateTime fechaPublicacion)
        {
            Publicar(fechaPost, urlSlug, esRssAtom);
            FechaPublicacion = fechaPublicacion;
        }

        public void ModificarTitulo(string titulo)
        {
            Titulo = titulo;
            TituloSinAcentos = titulo.RemoveDiacritics();
        }

      
    }
}
