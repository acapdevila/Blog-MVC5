using System.Data.Entity;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
using Blog.Modelo.Recetas;
using Blog.Modelo.Tags;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;

namespace Blog.Servicios.Blog.Borradores
{
    public class EditorBorradorPost
    {
        private readonly ContextoBaseDatos _db;
        private readonly BuscadorBlog _buscadorBlog;
        private readonly BuscadorBorrador _buscadorBorrador;
        private readonly AsignadorTags _asignadorTags;
        private readonly AsignadorCategorias _asignadorCategorias;

        public EditorBorradorPost(ContextoBaseDatos db, 
            BuscadorBlog buscadorBlog, 
            BuscadorBorrador buscadorBorrador,
            AsignadorTags asignadorTags, 
            AsignadorCategorias asignadorCategorias)
        {
            _db = db;
            _buscadorBlog = buscadorBlog;
            _buscadorBorrador = buscadorBorrador;
            _asignadorTags = asignadorTags;
            _asignadorCategorias = asignadorCategorias;
        }

        public Post GenerarNuevoBorrador(string autor)
        {
            var blog = _buscadorBlog.RecuperarBlog();
            return Post.CrearNuevoPorDefecto(autor, blog);
        }

        public Post GenerarNuevoBorradorPorReceta(Receta receta)
        {
            var blog = _buscadorBlog.RecuperarBlog();
            var post = Post.CrearNuevoPorDefecto(receta.Autor, blog);
            post.AsignarReceta(receta);
            return post;
        }

        public async Task CrearBorrador(EditorBorrador editorBorrador, Receta receta = null)
        {
            var blog = _buscadorBlog.RecuperarBlog();

            var post = Post.CrearNuevoPorDefecto(editorBorrador.Autor, blog);
            post.ActualizaBorrador(editorBorrador, _asignadorTags, _asignadorCategorias);

            post.AsignarReceta(receta);
            
            _db.Posts.Add(post);
            await _db.GuardarCambios();
            editorBorrador.Id = post.Id;
        }

        public async Task ActualizarBorrador(EditorBorrador editorBorrador, Receta receta = null)
        {
            var post = await _buscadorBorrador.BuscarBorradorPorIdAsync(editorBorrador.Id);
            post.ActualizaBorrador(editorBorrador, _asignadorTags, _asignadorCategorias);
            post.AsignarReceta(receta);
            await _db.GuardarCambios();
        }

        public async Task EliminarBorrador(Post borrador)
        {
            _db.Posts.Remove(borrador);
            await _db.GuardarCambios();
        }

        public async Task ActualizarNombresSinAcentos()
        {
            var posts = await _db.Posts.ToListAsync();
            var tags = await _db.Tags.ToListAsync();
            var categorias = await _db.Categorias.ToListAsync();

            foreach (var post in posts)
            {
                post.ModificarTitulo(post.Titulo);
            }

            foreach (var categoria in categorias)
            {
                categoria.CambiarNombre(categoria.Nombre);
            }

            foreach (var tag in tags)
            {
                tag.CambiarNombre(tag.Nombre);
            }

            await _db.SaveChangesAsync();
        }
    }
}
