using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blog.Modelo.Categorias;
using Blog.Modelo.Extensiones;
using Blog.Modelo.Recetas;
using Blog.Modelo.Tags;
using Blog.Modelo.Utensilios;

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
            PostRelacionados = new List<PostRelacionado>();
            Utensilios = new List<PostUtensilio>();
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

        public ICollection<PostRelacionado> PostRelacionados { get; set; }
        public ICollection<PostUtensilio> Utensilios { get; set; }

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

                sb.Append($"<p>{receta.Descripcion}</p>");

                sb.Append($"<h2>{receta.Nombre}</h2>");

                sb.Append($"<p class='yield smaller'><span class='glyphicon glyphicon-cutlery'>&nbsp;</span>Raciones: {receta.Raciones}</p>");
                
                sb.Append($"<p class='recipetime smaller'><span class='glyphicon glyphicon-time'>&nbsp;</span>Preparaci�n: {receta.TiempoPreparacion.FormatoHorasMinutos()}</p>");

                sb.Append($"<p class='recipetime smaller'><span class='glyphicon glyphicon-time'>&nbsp;</span>Cocci�n: {receta.TiempoCoccion.FormatoHorasMinutos()}</p>");

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


        public List<PostRelacionado> AsignarPostsRelacionados(List<Post> posts)
        {
            var listaPostsAsignados = new List<PostRelacionado>();

            if (posts == null) return listaPostsAsignados;

            int i = 1;
            foreach (var post in posts)
            {
                var nuevaAsignacion = AsignarPostRelacionado(post, i);
                i++;

                if(nuevaAsignacion != null)
                    listaPostsAsignados.Add(nuevaAsignacion);
            }

            return listaPostsAsignados;

        }

        private PostRelacionado AsignarPostRelacionado(Post post, int posicion)
        {
            var postExistente = PostRelacionados.FirstOrDefault(p => p.HijoId == post.Id);

            if (postExistente != null)
            {
                postExistente.Posicion = posicion;
                return null;
            }

            var postRelacionado = new PostRelacionado
            {
                PostId = Id,
                HijoId = post.Id,
                Posicion = posicion
            };


            PostRelacionados.Add(postRelacionado);

            return postRelacionado;


        }

        public List<PostRelacionado> QuitarPostsDiferentesA(List<Post> posts)
        {
            if(posts == null) return new List<PostRelacionado>();

            var postsAEliminar = PostRelacionados.Where(m => posts.All(p => m.HijoId != p.Id)).ToList();

            foreach (var postAEliminar in postsAEliminar)
            {
                PostRelacionados.Remove(postAEliminar);
            }

            return postsAEliminar.ToList();
        }


        public List<PostUtensilio> AsignarUtensilios(List<Utensilio> utensilios)
        {
            var listaUtensiliosAsignados = new List<PostUtensilio>();

            if (utensilios == null) return listaUtensiliosAsignados;

            int i = 1;
            foreach (var utensilio in utensilios)
            {
                var nuevaAsignacion = AsignarUtensilio(utensilio, i);
                i++;

                if (nuevaAsignacion != null)
                    listaUtensiliosAsignados.Add(nuevaAsignacion);
            }

            return listaUtensiliosAsignados;

        }


        private PostUtensilio AsignarUtensilio(Utensilio utensilio, int posicion)
        {
            var utensilioExistente = Utensilios.FirstOrDefault(p => p.UtensilioId == utensilio.Id);

            if (utensilioExistente != null)
            {
                utensilioExistente.Posicion = posicion;
                return null;
            }

            var postUtensilio = new PostUtensilio()
            {
                PostId = Id,
                UtensilioId = utensilio.Id,
                Posicion = posicion
            };


            Utensilios.Add(postUtensilio);

            return postUtensilio;


        }


        public List<PostUtensilio> QuitarUtensiliosDiferentesA(List<Utensilio> utensilios)
        {
            if (utensilios == null) return new List<PostUtensilio>();

            var aEliminar = Utensilios.Where(m => utensilios.All(p => m.UtensilioId != p.Id)).ToList();

            foreach (var utensilioAEliminar in aEliminar)
            {
                Utensilios.Remove(utensilioAEliminar);
            }

            return aEliminar.ToList();
        }
    }
}
