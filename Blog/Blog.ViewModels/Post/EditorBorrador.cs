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
    public class EditorBorrador
    {
        private string _urlSlug;


        public EditorBorrador()
        {
            
        }
        public EditorBorrador(Modelo.Posts.Post post): this()
        {
            this.InjectFrom(post);
            Tags = post.Tags.TagsSeparadosPorComma();
            Categorias = post.Categorias.CategoriasSeparadasPorComma();
            Receta = post.Receta?.Nombre;
        }

      
        public int Id { get; set; }

        [AllowHtml]
        [Display(Name = "Imagen")]
        public string Subtitulo { get; set; }

        [Display(Name = "Título")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        [Required(ErrorMessage = "Escribe un título")]
        public string Titulo { get; set; }


        [Display(Name = "Descripción - 110 palabras máx")]
        [StringLength(512, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Descripcion { get; set; }

        [Display(Name = "Palabras clave")]
        [StringLength(256, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string PalabrasClave { get; set; }

        [Display(Name = "Url imagen principal de la página")]
        public string UrlImagenPrincipal { get; set; }

        [Display(Name = "Url del post")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        [Required(ErrorMessage = "Escribe una url")]
        public string UrlSlug
        {
            get { return _urlSlug; }
            set { _urlSlug = string.IsNullOrEmpty(value) ? value : value.Replace(" ", "-") ; }
        }

        [Display(Name = @"Fecha")]
        [Required(ErrorMessage = @"Escribe una fecha")]
        public DateTime FechaPost { get; set; }
        
        [AllowHtml]
        [Display(Name = "Contenido")]
        public string ContenidoHtml { get; set; }


        [Required]
        public string Autor { get; set; }

        public string Receta { get; set; }


        public List<EditorPostRelacionado> PostsRelacionados { get; set; }


        [Display(Name = "Etiquetas")]
        public string Tags { get; set; }

        public string Categorias { get; set; }

        public List<string> ListaTags => string.IsNullOrEmpty(Tags) ? new List<string>() : Tags.Split(ExtensionesTag.SeparadorTags).ToList();

        public List<string> ListaCategorias => string.IsNullOrEmpty(Categorias) ? new List<string>() : Categorias.Split(new[] { ExtensionesCategoria.SeparadorCategorias }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
