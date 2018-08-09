﻿using System;
using System.ComponentModel.DataAnnotations;
using Omu.ValueInjecter;

namespace Blog.ViewModels.Post
{
    public class PublicarPost
    {
        public PublicarPost(Modelo.Posts.Post post)
        {
            this.InjectFrom(post);
        }
        public int Id { get; set; }
        
        [Display(Name = @"Fecha")]
        [Required(ErrorMessage = @"Escribe una fecha")]
        public DateTime FechaPost { get; set; }
        
        [Display(Name = "Rss Atom")]
        public bool EsRssAtom { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "Escribe una fecha")]
        public DateTime FechaPublicacion { get; set; }


  }
}
