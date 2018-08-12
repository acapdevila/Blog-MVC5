using System.Collections.Generic;
using System.Linq;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

namespace Blog.Servicios.Rutas
{
    public  class BuscadorRutas
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;

        public BuscadorRutas(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }


        private IQueryable<Post> Posts()
        {
            return _db.Posts
                .Where(m => m.Blog.Titulo == _tituloBlog);

        }

        private IQueryable<Categoria> Categorias()
        {
            return _db.Categorias
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));

        }

        private IQueryable<Tag> Etiquetas()
        {
            return _db.Tags
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));

        }

        public bool Existe(string urlSlug)
        {
            return Posts().Any(m => m.UrlSlug.ToLower() == urlSlug);
        }

        public bool ExisteCategoria(string urlCategoria)
        {
            return Categorias().Any(m => m.UrlSlug.ToLower() == urlCategoria);
        }

        public bool ExisteEtiqueta(string urlEtiqueta)
        {
            return Etiquetas().Any(m => m.UrlSlug.ToLower() == urlEtiqueta);
        }

        public List<RutaDto> BuscarRutasDePosts()
        {
            return 
                Posts()
                    .Publicados()
                    .Where(m => m.UrlSlug != null && m.UrlSlug != "")
                    .OrdenadosPorFecha()
                        .Select(m => new RutaDto
                    {
                        UrlSlug =  m.UrlSlug,
                        FechaPublicacion = m.FechaPublicacion
                    })
                    .ToList();
        }

        public List<RutaDto> BuscarRutasDeCategorias()
        {
            return
                Categorias()
                    .ConPostsPublicados()
                    .Where(m=>m.UrlSlug != null && m.UrlSlug != "")
                    .OrderBy(m=>m.UrlSlug)
                    .Select(m => new RutaDto
                    {
                        UrlSlug = m.UrlSlug,
                        FechaPublicacion = m.Posts.OrderByDescending(p=>p.FechaPublicacion).FirstOrDefault().FechaPublicacion
                    })
                    .ToList();
        }

        public List<RutaDto> BuscarRutasDeEtiquetas()
        {
            return
                Etiquetas().ConPostsPublicados()
                    .Where(m => m.UrlSlug != null && m.UrlSlug != "")
                    .OrderBy(m => m.UrlSlug)
                    .Select(m => new RutaDto
                    {
                        UrlSlug = m.UrlSlug,
                        FechaPublicacion = m.Posts.OrderByDescending(p => p.FechaPublicacion).FirstOrDefault().FechaPublicacion
                    })
                    .ToList();
        }
    }
}
