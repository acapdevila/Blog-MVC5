using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Blog.Modelo;

namespace Blog.Web.ViewModels.Post
{
    public class EditorPost
    {
        public EditorPost()
        {
          
        }
        public int Id { get; set; }

        [AllowHtml]
        [Display(Name = "Subtítulo")]
        [Required(ErrorMessage = "Escribe un subtítulo")]
        public string Subtitulo { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Escribe un título")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Titulo { get; set; }

        [Display(Name = "Url del post")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string UrlSlug { get; set; }

        [Display(Name = @"Fecha")]
        [Required(ErrorMessage = @"Escribe una fecha")]
        public DateTime FechaPost { get; set; }
        
        [AllowHtml]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContenidoHtml { get; set; }

        [Display(Name = "Borrador")]
        public bool EsBorrador { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "Escribe una fecha")]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public string Autor { get; set; }
        
        public string Tags { get; set; }

        public List<string> ListaTags => string.IsNullOrEmpty(Tags) ? new List<string>() : Tags.Split(Tag.Separador).ToList();
    }
}
