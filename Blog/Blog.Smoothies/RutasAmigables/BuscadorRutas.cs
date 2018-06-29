using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;

namespace Blog.Smoothies.RutasAmigables
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


        public IQueryable<Post> Posts()
        {
            return _db.Posts
                .Where(m => m.Blog.Titulo == _tituloBlog);

        }

        public IQueryable<Categoria> Categorias()
        {
            return _db.Categorias
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
    }
}
