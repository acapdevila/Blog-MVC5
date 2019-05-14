using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;
using Omu.ValueInjecter;

namespace Blog.ViewModels.Post
{
    public class EditorPost
    {
        public EditorPost()
        {
          PostsRelacionados = new List<EditorPostRelacionado>();
          Utensilios = new List<EditorPostUtensilio>();
        }

        public EditorPost(Modelo.Posts.Post post): this()
        {
           this.InjectFrom(post);
           Tags = post.Tags.TagsSeparadosPorComma();
           Categorias = post.Categorias.CategoriasSeparadasPorComma();
            Receta = post.Receta?.Nombre;
           AñadirPostsRelacionados(post);
           AñadirUtensilios(post);

            
        }

        public EditorPost(EditorBorrador viewModel)
        {
            this.InjectFrom(viewModel);
        }

        public int Id { get; set; }
        [AllowHtml]
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Escribe un subtítulo")]
        public string Subtitulo { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Escribe un título")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Titulo { get; set; }


        [Display(Name = "Descripción - 110 palabras máx")]
        [Required(ErrorMessage = "Escribe una descripción")]
        [StringLength(512, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Descripcion { get; set; }

        [Display(Name = "Palabras clave")]
        [Required(ErrorMessage = "Escribe las palabras clave del post")]
        [StringLength(256, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string PalabrasClave { get; set; }

        [Display(Name = "Url imagen principal de la página")]
        [Required(ErrorMessage = "Escribe la url de la imagen principal")]
        public string UrlImagenPrincipal { get; set; }

        [Display(Name = "Url del post")]
        public string UrlSlug
        {
            get; 
        set;
        }

        [Display(Name = @"Fecha")]
        [Required(ErrorMessage = @"Escribe una fecha")]
        public DateTime FechaPost { get; set; }
        
        [AllowHtml]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContenidoHtml { get; set; }


        [Display(Name = "Rss Atom")]
        public bool EsRssAtom { get; set; }

        [Display(Name = "Fecha de publicación")]
        public DateTime FechaPublicacion { get; set; }
        
        [Required]
        public string Autor { get; set; }

        public string Receta { get; set; }


        [Display(Name = "Etiquetas")]
        public string Tags { get; set; }

        public string Categorias { get; set; }


        [Display(Name = "Posts relacionados")]
        public List<EditorPostRelacionado> PostsRelacionados { get; set; }

        [Display(Name = "Utensilios")]
        public List<EditorPostUtensilio> Utensilios { get; set; }

        public List<string> ListaTags => string.IsNullOrEmpty(Tags) ? new List<string>() : Tags.Split(ExtensionesTag.SeparadorTags).ToList();

        public List<string> ListaCategorias => string.IsNullOrEmpty(Categorias) ? new List<string>() : Categorias.Split(new[] { ExtensionesCategoria.SeparadorCategorias }, StringSplitOptions.RemoveEmptyEntries).ToList();


        private void AñadirPostsRelacionados(Modelo.Posts.Post post)
        {
            foreach (var postRelacionado in post.PostRelacionados.OrderBy(m => m.Posicion))
            {
                PostsRelacionados.Add(new EditorPostRelacionado(postRelacionado));
            }

        }

        private void AñadirUtensilios(Modelo.Posts.Post post)
        {
            //PostsRelacionados = new List<EditorPostRelacionado>();

            foreach (var postUtensilio in post.Utensilios.OrderBy(m => m.Posicion))
            {
                Utensilios.Add(new EditorPostUtensilio(postUtensilio));
            }

        }

    }
}
