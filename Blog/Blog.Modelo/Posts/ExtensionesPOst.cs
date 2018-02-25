using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Categorias;
using Blog.Modelo.Tags;

namespace Blog.Modelo.Posts
{
   public static class ExtensionesPost
    {
       public static IQueryable<Post> Publicados(this IQueryable<Post> posts)
        {
           return posts.Where(m => !m.EsBorrador && m.FechaPublicacion <= DateTime.Now);
        }

        public static IQueryable<Post> DeCategorias(this IQueryable<Post> posts,ICollection<Categoria> categorias)
        {
            List<int> idsCategorias = categorias.Select(m => m.Id).ToList();
            return posts.Where(m => m.Categorias.Any(c=> idsCategorias.Contains(c.Id)));
        }

        public static IQueryable<Post> DeTags(this IQueryable<Post> posts,ICollection<Tag> tags)
        {
            List<int> idsTags = tags.Select(m => m.Id).ToList();
            return posts.Where(m => m.Tags.Any(c => idsTags.Contains(c.Id)));
        }

        public static IQueryable<Post> Anteriores(this IQueryable<Post> posts, Post post)
        {
            return posts.Where(m => m.FechaPost < post.FechaPost);
        }

        public static IQueryable<Post> Posteriores(this IQueryable<Post> posts, Post post)
        {
            return posts.Where(m => post.FechaPost < m.FechaPost);
        }

        public static IQueryable<Post> BuscarPor(this IQueryable<Post> posts, CriteriosBusqueda criterios)
        {
            if(criterios == CriteriosBusqueda.Vacio())
                    return posts;

            var consulta = posts;

            foreach (var palabraBuscada in criterios.PalabrasBuscadas.Distinct())
            {
                consulta = consulta.Where(m => m.Titulo.Contains(palabraBuscada) || palabraBuscada.Contains(m.Titulo));
            }

            foreach (var tag in criterios.Tags.Distinct())
            {
                consulta = consulta.Where(m => m.Tags.Any(t => t.Nombre == tag.Nombre));
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
