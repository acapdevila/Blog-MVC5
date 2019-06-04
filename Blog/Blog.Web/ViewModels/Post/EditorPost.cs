using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Ac.Modelo.Tags;
using Omu.ValueInjecter;

namespace Ac.ViewModels.Post
{
    public class EditorPost
    {
        public EditorPost()
        {
        }

        public EditorPost(Modelo.Post post): this()
        {
           this.InjectFrom(post);
           Tags = post.Tags.TagsSeparadosPorComma();
           

            
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

        
        [Display(Name = "Etiquetas")]
        public string Tags { get; set; }
        

        public List<string> ListaTags => string.IsNullOrEmpty(Tags) ? new List<string>() : Tags.Split(ExtensionesTag.SeparadorTags).ToList();

    
    

    }
}
