using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Servicios.Comun;

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
            if(!titulos.Any()) return new List<Post>();

            var posts = await _db.Posts.Where(m => titulos.Any(t => t.ToLower() == m.Titulo.ToLower())).ToListAsync();

            if (!posts.Any()) return posts;

            var postsOrdenados = new List<Post>();

            foreach (var titulo in titulos)
            {
                var postEncontrado = posts.FirstOrDefault(m => m.Titulo.ToLower() == titulo.ToLower());
                if (postEncontrado != null) 
                    postsOrdenados.Add(postEncontrado);
            }

            return postsOrdenados;
        }
        
        public async Task<List<ElementoValorDescripcion>> TitulosDePosts(string contiene = null, int maximoElementos = 10)
        {
            return await _db.Posts
                .Where(m => m.Titulo.Contains(contiene))
                .OrderBy(m => m.Titulo)
                .Take(maximoElementos)
                .Select(m => new ElementoValorDescripcion
                {
                    Valor = m.Id,
                    Descripcion = m.Titulo
                })
                .ToListAsync();

        }
    }
}
