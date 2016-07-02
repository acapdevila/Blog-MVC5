using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.ViewModels.Blog;
using PagedList;

namespace Blog.Servicios
{
    public class BlogServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;
        public BlogServicio(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts
                .Include(m => m.Tags)
                .Where(m => m.Blog.Titulo == _tituloBlog);
        }

        private IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any(p=>p.Blog.Titulo == _tituloBlog));
        }

        public ListaPostsBlogViewModel ObtenerListaPostsBlogViewModel(int pagina, int nummeroItemsPorPagina)
        {
            return new ListaPostsBlogViewModel
            {
                ListaPosts = Posts()
                .Publicados()
                .SeleccionaLineaResumenPost()
                .OrderByDescending(m => m.FechaPost)
                .ToPagedList(pagina, nummeroItemsPorPagina)
            };
        }

        public async Task<Tag> RecuperarTagConPostsRelacionados(string urlSlug)
        {
            return await Tags()
                .ConPostsPublicados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }

        public async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await Posts()
                            .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug && m.FechaPost == fechaPost);
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
