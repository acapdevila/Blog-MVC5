using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.ViewModels.Post
{
    public class EditorBlog
    {
        public EditorBlog()
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

        [Display(Name = "Url del blog")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string UrlSlug { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContenidoHtml { get; set; }

        [Required]
        public string Autor { get; set; }
        
      
    }
}
