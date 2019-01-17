using System;
using System.Collections.Generic;
using System.Text;
using Blog.Modelo.Categorias;
using Blog.Modelo.Extensiones;
using Blog.Modelo.Recetas;
using Blog.Modelo.Tags;

namespace Blog.Modelo.Posts
{
    public class Post : IEntidadConTags
    {
        public static Post CrearNuevoPorDefecto(string autor,BlogEntidad blog)
        {
            return new Post
            {
                BlogId = blog.Id,
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

        public Receta Receta { get;private set; }

        public string TituloSinAcentos { get; private set; }

        public bool EsPublico => !EsBorrador && FechaPublicacion <= DateTime.Now;

        public bool EsReceta => Receta != null;

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


        public void AsignarReceta(Receta receta)
        {
            Receta = receta;

            if(receta == null) return;
            
            if (string.IsNullOrEmpty(Titulo))
                Titulo = receta.Nombre;

            if (string.IsNullOrEmpty(PalabrasClave))
                PalabrasClave = receta.Keywords;

            if (string.IsNullOrEmpty(Descripcion))
                Descripcion = receta.Descripcion;


            if (string.IsNullOrEmpty(UrlImagenPrincipal))
                UrlImagenPrincipal = receta.Imagen.Url;

            if (string.IsNullOrEmpty(Subtitulo))
            {
                Subtitulo =
                    $"<p><img alt='{receta.Imagen.Alt}' class='img-responsive' src='{receta.Imagen.Url}' /></p>";
            }

            if (string.IsNullOrEmpty(ContenidoHtml))
            {
                var sb = new StringBuilder();

                sb.Append($"<h2>{receta.Nombre}</h2>");

                sb.Append($"<p class='yield smaller'><span class='glyphicon glyphicon-cutlery'>&nbsp;</span>Raciones: {receta.Raciones}</p>");
                
                sb.Append($"<p class='recipetime smaller'><span class='glyphicon glyphicon-time'>&nbsp;</span>Preparación: {receta.TiempoPreparacion.FormatoHorasMinutos()}</p>");

                sb.Append($"<p class='recipetime smaller'><span class='glyphicon glyphicon-time'>&nbsp;</span>Cocción: {receta.TiempoCoccion.FormatoHorasMinutos()}</p>");

                sb.Append($"<p class='recipetime smaller'><span class='glyphicon glyphicon-time'>&nbsp;</span>Total: {receta.TiempoTotal.FormatoHorasMinutos()}</p>");

                sb.Append("<h3>Ingredientes</h3>");
                sb.Append("<ul>");

                foreach (var recetaIngrediente in receta.Ingredientes)
                {
                    sb.Append($"<li>{recetaIngrediente.Nombre}</li>");
                }
                sb.Append("</ul>");

                sb.Append("<h3>Instrucciones</h3>");
                sb.Append("<ol>");

                foreach (var instruccion in receta.Instrucciones)
                {
                    sb.Append($"<li>{instruccion.Nombre}</li>");
                }

                sb.Append("</ol>");

                ContenidoHtml = sb.ToString();
            }

         

        }


    }
}
