using System;
using System.Collections.Generic;
using System.Linq;
using Ac.Modelo.Posts;
using Ac.Modelo.Tags;
using Infra;

namespace Ac.Modelo
{
    public class Post : IEntidadConTags
    {
        public static Post CrearNuevoPorDefecto(string autor)
        {
            return new Post
            {
                Autor = autor,
                EsBorrador = true,
                EsRssAtom = false,
                FechaPost = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddMonths(6)
            };
        }

        public Post()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }
        
        public string Subtitulo { get; set; }
        public string Titulo { get; private set; }

        public string Descripcion { get; set; }

        public string PalabrasClave { get; set; }

        public string UrlImagenPrincipal { get; set; }

        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public string ContenidoHtml { get; set; }

        public string PostContenidoHtml { get; set; }
        public bool EsBorrador { get; private set; }
        public bool EsRssAtom { get; private set; }
        public DateTime FechaPublicacion { get; private set; }

        public DateTime FechaModificacion { get; set; }

        public string Autor { get; set; }
        
        public ICollection<Tag> Tags { get; set; }

    
        public string TituloSinAcentos { get; private set; }


       

        public bool EsPublico => !EsBorrador && FechaPublicacion <= DateTime.Now;


        public bool EsMostrarDatosEstructurados
        {
            get { return !string.IsNullOrEmpty(UrlImagenPrincipal) && !string.IsNullOrEmpty(Descripcion); }
        }

        
        public void Publicar(DateTime fechaPost, string urlSlug, bool esRssAtom)
        {
            FechaPost = fechaPost;

            UrlSlug = urlSlug.Replace(" ", "-");

            if(DateTime.Now < FechaPublicacion)
                FechaPublicacion = DateTime.Now.AddHours(-2).AddMinutes(-1);

            EsRssAtom = esRssAtom;

            EsBorrador = false;
        }

        public void ProgramarPublicacion(DateTime fechaPost, string urlSlug, bool esRssAtom, DateTime fechaPublicacion)
        {
            Publicar(fechaPost, urlSlug, esRssAtom);
            FechaPublicacion = fechaPublicacion;
        }

        public void ModificarTitulo(string titulo)
        {
            Titulo = titulo;
            TituloSinAcentos = titulo.RemoveDiacritics();
        }


    }


    public static class ExtensionesPost
    {
        public static IQueryable<Post> Borradores(this IQueryable<Post> posts)
        {
            return posts.Where(m => m.EsBorrador || DateTime.Now < m.FechaPublicacion);
        }

        public static IQueryable<Post> Publicados(this IQueryable<Post> posts)
        {
            return posts.Where(m => !m.EsBorrador && m.FechaPublicacion <= DateTime.Now);
        }

        public static IQueryable<Post> OrdenadosPorFecha(this IQueryable<Post> posts)
        {
            return posts.OrderByDescending(m => m.FechaPost);
        }


        public static IQueryable<Post> DeTags(this IQueryable<Post> posts, ICollection<Tag> tags)
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

        
        public static IQueryable<Post> BuscarPor(this IQueryable<Post> posts, CriteriosBusqueda criterios, List<Tag> tags)
        {
            if (criterios == CriteriosBusqueda.Vacio())
                return posts;

            var palabrasaSinAcento = criterios.PalabrasBuscadasSinAcento;

            var consulta = posts.Where(m => palabrasaSinAcento.Any(p => p.Contains(m.TituloSinAcentos) || m.TituloSinAcentos.Contains(p)));

            if (tags.Any())
            {
                var idsTags = tags.Select(m => m.Id).ToList();
                consulta = consulta.Union(posts.Where(m => m.Tags.Any(t => idsTags.Contains(t.Id))));
            }

            return consulta;
        }

        public static IQueryable<Post> BuscarPorTags(this IQueryable<Post> consulta, List<Tag> tags)
        {
            foreach (var tag in tags.Distinct())
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
                UrlImagen = m.UrlImagenPrincipal,
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
