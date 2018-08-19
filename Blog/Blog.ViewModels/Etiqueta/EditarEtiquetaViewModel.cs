﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Blog.Modelo.Dtos;
using Blog.Modelo.Tags;

namespace Blog.ViewModels.Etiqueta
{
    public  class EditarEtiquetaViewModel
    {
        public EditarEtiquetaViewModel() : this(new Tag())
        {
            
        }

        public EditarEtiquetaViewModel(Tag tag)
        {
            Id = tag.Id;
            Nombre = tag.Nombre;
            UrlSlug = tag.UrlSlug;
            Descripcion = tag.Descripcion;
            ContenidoHtml = tag.ContenidoHtml;
            PalabrasClave = tag.PalabrasClave;
            UrlImagenPrincipal = tag.UrlImagenPrincipal;
            
        }

        private string _urlSlug;

        public int Id { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Escribe un nombre")]
        [StringLength(64, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Nombre { get; set; }


        [Display(Name = "Url del post")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string UrlSlug
        {
            get { return _urlSlug; }
            set { _urlSlug = string.IsNullOrEmpty(value) ? value : value.Replace(" ", "-"); }
        }

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


        [AllowHtml]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContenidoHtml { get; set; }
    }


    public static class EditarEtiquetaViewModelExtensiones
    {
        public static EtiquetaDto ToDto(this EditarEtiquetaViewModel editor)
        {
            return new EtiquetaDto
            {
                Nombre = editor.Nombre,
                UrlSlug = editor.UrlSlug,
                Id = editor.Id,
                Descripcion = editor.Descripcion,
                UrlImagenPrincipal = editor.UrlImagenPrincipal,
                PalabrasClave = editor.PalabrasClave,
                ContenidoHtml = editor.ContenidoHtml
               
                };
            
        }
    }

}
