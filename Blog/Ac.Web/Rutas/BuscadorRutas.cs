using System.Collections.Generic;
using System.Linq;
using Ac.Datos;
using Ac.Modelo;
using Ac.Modelo.Tags;

namespace Ac.Web.Rutas
{
    public  class BuscadorRutas
    {
        private readonly ContextoBaseDatos _db;
        
        public BuscadorRutas(ContextoBaseDatos db)
        {
            _db = db;
        }


        private IQueryable<Post> Posts()
        {
            return _db.Posts;

        }

        
        private IQueryable<Tag> Etiquetas()
        {
            return _db.Tags
                .Where(m => m.Posts.Any());

        }

        public bool Existe(string urlSlug)
        {
            return Posts().Any(m => m.UrlSlug.ToLower() == urlSlug);
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


        public List<RutaDto> BuscarRutasDeEtiquetas()
        {
            return
                Etiquetas()
                    //.ConPostsPublicados()
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
