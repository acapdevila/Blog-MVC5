using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;

namespace Blog.Servicios.Blog
{
    public class BuscadorPostsRelacionados
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorPostsRelacionados(ContextoBaseDatos db)
        {
            _db = db;
        }

        public async Task<List<Post>> BuscarPostsRelacionadosPorTitulosAsync(List<string> titulos)
        {
            var posts = await _db.Posts.Where(m => titulos.Any(t => t == m.Titulo)).ToListAsync();

            return posts;
        }
    }
}
