﻿using System;
using System.Linq;

namespace Blog.Modelo.Posts
{
   public static class ExtensionesPost
    {
       public static IQueryable<Post> Publicados(this IQueryable<Post> posts)
       {
           return posts.Where(m => !m.EsBorrador && m.FechaPublicacion <= DateTime.Now);
        }
        
        public static IQueryable<Post> ConUrlTag(this IQueryable<Post> posts, string urlSlugTag)
        {
            return posts.Where(m => m.Tags.Any(t => t.UrlSlug.ToLower() == urlSlugTag));
        }
        

       public static IQueryable<LineaResumenPost> SeleccionaLineaResumePost(this IQueryable<Post> posts)
       {
           return posts.Select(m => new LineaResumenPost
           {
               Id = m.Id,
               UrlSlug = m.UrlSlug,
               Titulo = m.Titulo,
               Subtitulo = m.Subtitulo,
               FechaPost = m.FechaPost,
               Autor = m.Autor
           });
       }
    }
}