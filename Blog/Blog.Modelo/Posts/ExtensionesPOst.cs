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

        public static IQueryable<Post> BuscarPor(this IQueryable<Post> posts, CriteriosBusqueda criterios)
        {
            if(criterios == CriteriosBusqueda.Vacio())
            return posts;

            var consulta = posts;

            foreach (var palabraBuscada in criterios.PalabrasBuscadas)
            {
                consulta = consulta.Where(m => m.Titulo.Contains(palabraBuscada) || palabraBuscada.Contains(m.Titulo) ||
                                               m.Tags.Any(t =>t.Nombre.Contains(palabraBuscada) || palabraBuscada.Contains(t.Nombre)));
            }

            return consulta;
        }

        public static IQueryable<Post> PublicadosRssAtom(this IQueryable<Post> posts)
        {
            return posts.Publicados().Where(m => m.EsRssAtom);
        }

        public static IQueryable<Post> ConUrlTag(this IQueryable<Post> posts, string urlSlugTag)
        {
            return posts.Where(m => m.Tags.Any(t => t.UrlSlug.ToLower() == urlSlugTag));
        }
        

       public static IQueryable<LineaResumenPost> SeleccionaLineaResumenPost(this IQueryable<Post> posts)
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

        public static IQueryable<LineaPostCompleto> SeleccionaLineaPostCompleto(this IQueryable<Post> posts)
        {
            return posts.Select(m => new LineaPostCompleto
            {
                Id = m.Id,
                UrlSlug = m.UrlSlug,
                Titulo = m.Titulo,
                Subtitulo = m.Subtitulo,
                FechaPost = m.FechaPost,
                Autor = m.Autor,
                ContenidoHtml = m.ContenidoHtml
            });
        }




    }
}
