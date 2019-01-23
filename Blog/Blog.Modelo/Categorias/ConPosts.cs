using System.Linq;

namespace Blog.Modelo.Categorias
{
    public static class ConPostsExtension
    {
        public static IQueryable<Categoria> ConPosts(this IQueryable<Categoria> categorias, string tituloBlog)
        {
            return categorias.Where(m => m.Posts.Any(p => p.Blog.Titulo == tituloBlog));
        }
    }
}
