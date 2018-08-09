using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
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
        private readonly AsignadorCategorias _asignadorCategorias;
        private readonly string _tituloBlog;

        public PostsServicio(ContextoBaseDatos db, AsignadorTags asignadorTags, AsignadorCategorias asignadorCategorias, string tituloBlog)
        {
            _db = db;
            _asignadorTags = asignadorTags;
            _asignadorCategorias = asignadorCategorias;
            _tituloBlog = tituloBlog;
            
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts
                .Where(m => m.Blog.Titulo == _tituloBlog);
        }

        private IQueryable<Post> PostsPublicados()
        {
            return Posts().Where(m => !m.EsBorrador);
        }

        private IQueryable<Post> Borradores()
        {
            return Posts().Where(m=>m.EsBorrador);
        }

        private BlogEntidad RecuperarBlog()
        {
            return _db.Blogs.First(m => m.Titulo == _tituloBlog);
        }


        public async Task<Post> RecuperarPost(int id)
        {
            return await Posts()
                .Include(m => m.Tags)
                .Include(m => m.Categorias)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(CriteriosBusqueda criteriosBusqueda, int numeroPagina, int postsPorPagina)
        {
            return new ListaGestionPostsViewModel
            {
                BuscarPor = criteriosBusqueda,
                ListaPosts = await PostsPublicados()
                    .BuscarPor(criteriosBusqueda)
                    .Select(m => new LineaGestionPost
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    FechaPost = m.FechaPost,
                    EsBorrador = m.EsBorrador,
                    EsRssAtom = m.EsRssAtom,
                    FechaPublicacion = m.FechaPublicacion,
                    Autor = m.Autor,
                    ListaTags = m.Tags,
                    ListaCategorias = m.Categorias
                })
                .OrderByDescending(m => m.FechaPost)
                .ToPagedListAsync(numeroPagina, postsPorPagina)
            };
        }


        public async Task<List<LineaBorrador>> ObtenerListaBorradores(CriteriosBusqueda criteriosBusqueda)
        {
            return await Borradores()
                .BuscarPor(criteriosBusqueda)
                .Select(m => new LineaBorrador
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    FechaPost = m.FechaPost,
                    FechaPublicacion = m.FechaPublicacion,
                    Autor = m.Autor,
                    ListaTags = m.Tags,
                    ListaCategorias = m.Categorias
                })
                .OrderByDescending(m => m.FechaPost)
                .ToListAsync();
            ;
        }


        public async Task CrearBorrador(EditorBorrador editorBorrador)
        {
            var blog = RecuperarBlog();

            var post = Post.CrearNuevoPorDefecto(editorBorrador.Autor, blog.Id);
            post.ActualizaBorrador(editorBorrador, _asignadorTags, _asignadorCategorias);
            _db.Posts.Add(post);
            await _db.GuardarCambios();
            editorBorrador.Id = post.Id;
        }

        
        public async Task ActualizarPost(EditorPost editorPost)
        {
            var post = await RecuperarPost(editorPost.Id);
            post.ActualizaPost(editorPost, _asignadorTags, _asignadorCategorias);
            await _db.GuardarCambios();
           
           
        }

        public async Task ActualizarBorrador(EditorBorrador editorBorrador)
        {
            var post = await RecuperarPost(editorBorrador.Id);
            post.ActualizaBorrador(editorBorrador, _asignadorTags, _asignadorCategorias);
            await _db.GuardarCambios();
        }

        public async Task PublicarPost(PublicarPost editor)
        {
            var post = await RecuperarPost(editor.Id);
            post.Publicar(editor.FechaPost, editor.FechaPublicacion, editor.EsRssAtom);
            await _db.GuardarCambios();
        }

        public async Task EliminarPost(int id)
        {
            var post = await RecuperarPost(id);
            _db.Posts.Remove(post);
            await _db.GuardarCambios();
        }

        public void Dispose()
        {
            _db.Dispose();
         
        }

        public EditorPost ObtenerNuevoEditorPorDefecto(string autor)
        {
            var blog = RecuperarBlog();

            var post = Post.CrearNuevoPorDefecto(autor, blog.Id);

            var editor = new EditorPost();
            editor.ActualizaBorrador(post);
            return editor;
            
        }

       
    }
}
