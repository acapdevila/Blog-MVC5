using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;

namespace Blog.ViewModels.Post
{
    public class EditorPost
    {
        private string _urlSlug;

        public EditorPost()
        {
          
        }
        public int Id { get; set; }
        public int BlogId { get; set; }

        [AllowHtml]
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Escribe un subtítulo")]
        public string Subtitulo { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Escribe un título")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Titulo { get; set; }


        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Escribe una descripción")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Descripcion { get; set; }

        [Display(Name = "Url imagen principal de la página")]
        [Required(ErrorMessage = "Escribe la url de la imagen principal")]
        public string UrlImagenPrincipal { get; set; }

        [Display(Name = "Url del post")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
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
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContenidoHtml { get; set; }

        [AllowHtml]
        [Display(Name = "Eliminado")]
        public string PostContenidoHtml { get; set; }

        [Display(Name = "Borrador")]
        public bool EsBorrador { get; set; }

        [Display(Name = "Rss Atom")]
        public bool EsRssAtom { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "Escribe una fecha")]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public string Autor { get; set; }

        [AllowHtml]
        [Display(Name = "Datos estructurados")]
        [Required(ErrorMessage = "Introduce los datos estructurados")]
        public string DatosEstructurados { get; set; }


        [Display(Name = "Etiquetas / palabras clave")]
        public string Tags { get; set; }

        public string Categorias { get; set; }

        public List<string> ListaTags => string.IsNullOrEmpty(Tags) ? new List<string>() : Tags.Split(ExtensionesTag.SeparadorTags).ToList();

        public List<string> ListaCategorias => string.IsNullOrEmpty(Categorias) ? new List<string>() : Categorias.Split(ExtensionesCategoria.SeparadorCategorias).ToList();
    }
}
