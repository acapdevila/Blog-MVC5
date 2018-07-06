using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;

namespace Blog.Servicios
{
    public class BlogsServicio
    {
        private readonly ContextoBaseDatos _db;
      

        public BlogsServicio(ContextoBaseDatos db)
        {
            _db = db;
        }

        public async Task<ListaGestionBlogsViewModel> ObtenerListaBlogsViewModel()
        {
            return new ListaGestionBlogsViewModel
            {
                ListaBlogs = await _db.Blogs.Select(m => new LineaGestionBlog
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    Autor = m.Autor,
                })
                .OrderByDescending(m => m.Id)
                .ToListAsync()
            };
        }

        public async Task<BlogEntidad> RecuperarBlog(int id)
        {
            return await _db.Blogs
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<BlogEntidad> RecuperarBlogPorTitulo(string titulo)
        {
            return await _db.Blogs
                .FirstOrDefaultAsync(m => m.Titulo.ToLower() == titulo.ToLower());
        }

        public async Task CrearBlog(EditorBlog editorBlog)
        {
            var post = BlogEntidad.CrearNuevoPorDefecto();
            post.CopiaValores(editorBlog);
            _db.Blogs.Add(post);
            await _db.SaveChangesAsync();
            editorBlog.Id = post.Id;
        }

        public async Task ActualizarBlog(EditorBlog editorBlog)
        {
            var post = await RecuperarBlog(editorBlog.Id);
            post.CopiaValores(editorBlog);
            await _db.SaveChangesAsync();
        }

        public async Task EliminarBlog(int id)
        {
            var post = await RecuperarBlog(id);
            _db.Blogs.Remove(post);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
