using System.Linq;
using Blog.Datos;
using Blog.Modelo.Posts;

namespace Blog.Servicios.Blog
{
    public class BuscadorBlog
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _titulo;

        public BuscadorBlog(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _titulo = tituloBlog;
        }

        public BlogEntidad RecuperarBlog()
        {
            return _db.Blogs.First(m => m.Titulo == _titulo);
        }
    }
}
