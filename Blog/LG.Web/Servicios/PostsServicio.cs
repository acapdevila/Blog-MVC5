using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
using Blog.Modelo.Recetas;
using Blog.Modelo.Tags;
using Blog.Modelo.Utensilios;
using Blog.Servicios;
using LG.Web.ViewModels.Post;
using LG.Web.ViewModels.Post.Conversores;
using PagedList.EntityFramework;

namespace LG.Web.Servicios
{
    public class PostsServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly TagsServicio _tagsServicio;
        private readonly CategoriasServicio _categoriasServicio;
        private readonly AsignadorTags _asignadorTags;
        private readonly AsignadorCategorias _asignadorCategorias;

        public PostsServicio(ContextoBaseDatos db, 
            AsignadorTags asignadorTags, 
            AsignadorCategorias asignadorCategorias)
        {
            _db = db;
            _tagsServicio = new TagsServicio(db);
            _categoriasServicio = new CategoriasServicio(db);
            _asignadorTags = asignadorTags;
            _asignadorCategorias = asignadorCategorias;
            
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts;
        }

        
        public async Task<Post> RecuperarPost(int id)
        {
            return await Posts()
                .Include(m => m.Tags)
                .Include(m => m.Categorias)
                .Include(m=>m.Receta)
                .Include(m => m.PostRelacionados.Select(p => p.Hijo))
                .Include(m => m.Utensilios.Select(p => p.Utensilio))
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(CriteriosBusqueda criteriosBusqueda, int numeroPagina, int postsPorPagina)
        {
            var tags = await _tagsServicio.BuscarTags(criteriosBusqueda.PalabrasBuscadas);
            var categorias = await _categoriasServicio.BuscarCategoriasAsync(criteriosBusqueda.PalabrasBuscadas);
  
            return new ListaGestionPostsViewModel
            {
                BuscarPor = criteriosBusqueda.BuscarPor,
                ListaPosts = await Posts()
                    .Publicados()
                    .BuscarPor(criteriosBusqueda, tags, categorias)
                    .Select(m => new LineaGestionPost
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    FechaPost = m.FechaPost,
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

        
        
        public async Task ActualizarPost(EditorPost editorPost, Receta  receta, List<Post> postsRelacionados, List<Utensilio> utensilios)
        {
            var post = await RecuperarPost(editorPost.Id);
            post.ActualizaPost(editorPost, _asignadorTags, _asignadorCategorias);
            post.AsignarReceta(receta);
            
            var postDesasignados = post.QuitarPostsDiferentesA(postsRelacionados);
            _db.PostsRelacionados.RemoveRange(postDesasignados);
            var postAsignadosNuevos = post.AsignarPostsRelacionados(postsRelacionados);
            _db.PostsRelacionados.AddRange(postAsignadosNuevos);

            var utensiliosDesasignados = post.QuitarUtensiliosDiferentesA(utensilios);
            _db.PostsUtensilios.RemoveRange(utensiliosDesasignados);
            var utensiliosAsignadosNuevos = post.AsignarUtensilios(utensilios);
            _db.PostsUtensilios.AddRange(utensiliosAsignadosNuevos);


            await _db.GuardarCambios();
           
           
        }

       

        public async Task PublicarPost(PublicarPost editor)
        {
            var post = await RecuperarPost(editor.Id);
            post.Publicar(editor.FechaPost, editor.UrlSlug, editor.EsRssAtom);
            await _db.GuardarCambios();
        }

        public async Task ProgramarPublicacion(PublicarPost editor)
        {
            var post = await RecuperarPost(editor.Id);
            post.ProgramarPublicacion(editor.FechaPost, editor.UrlSlug, editor.EsRssAtom, editor.FechaPublicacion);
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


     
    }
}
