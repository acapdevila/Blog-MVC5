using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;
using PagedList.EntityFramework;


namespace Blog.Servicios
{
    public class PostsServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly AsignadorTags _asignadorTags;
        private readonly string _tituloBlog;

        public PostsServicio(ContextoBaseDatos db, AsignadorTags asignadorTags, string tituloBlog)
        {
            _db = db;
            _asignadorTags = asignadorTags;
            _tituloBlog = tituloBlog;
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts.Where(m => m.Blog.Titulo == _tituloBlog);
        }

        public async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(int numeroPagina, int postsPorPagina)
        {
            return new ListaGestionPostsViewModel
            {
                ListaPosts = await Posts().Select(m => new LineaGestionPost
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    FechaPost = m.FechaPost,
                    EsBorrador = m.EsBorrador,
                    EsRssAtom = m.EsRssAtom,
                    FechaPublicacion = m.FechaPublicacion,
                    Autor = m.Autor,
                    ListaTags = m.Tags
                })
                .OrderByDescending(m => m.FechaPost)
                .ToPagedListAsync(numeroPagina, postsPorPagina)
            };
        }

        public async Task<Post> RecuperarPost(int id)
        {
            return await Posts().Include(m => m.Tags)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearPost(EditorPost editorPost)
        {
            var post = Post.CrearNuevoPorDefecto(editorPost.Autor, editorPost.BlogId);
            post.CopiaValores(editorPost, _asignadorTags);
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            editorPost.Id = post.Id;
        }

        public async Task ActualizarPost(EditorPost editorPost)
        {
            var post = await RecuperarPost(editorPost.Id);
            post.CopiaValores(editorPost, _asignadorTags);
            await _db.SaveChangesAsync();
        }

        public async Task EliminarPost(int id)
        {
            var post = await RecuperarPost(id);
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
         
        }

        public EditorPost ObtenerNuevoEditorPorDefecto(string autor)
        {
            var blog = _db.Blogs.First(m => m.Titulo == _tituloBlog);

            var post = Post.CrearNuevoPorDefecto(autor, blog.Id);

            var editor = new EditorPost();
            editor.CopiaValores(post);

            return editor;
            
        }
    }
}
