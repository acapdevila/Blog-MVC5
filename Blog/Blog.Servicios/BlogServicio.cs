using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
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

        public IQueryable<Post> Posts()
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

        public IPagedList<LineaResumenPost> ObtenerListaResumenPostsPublicados(int pagina, int nummeroItemsPorPagina)
        {
            return Posts()
                .Publicados()
                .SeleccionaLineaResumenPost()
                .OrderByDescending(m => m.FechaPost)
                .ToPagedList(pagina, nummeroItemsPorPagina);
            
        }

        public IPagedList<LineaPostCompleto> ObtenerListaPostsCompletosPublicados(int pagina, int nummeroItemsPorPagina)
        {
            return Posts()
                .Publicados()
                .SeleccionaLineaPostCompleto()
                .OrderByDescending(m => m.FechaPost)
                .ToPagedList(pagina, nummeroItemsPorPagina);

        }

        public async Task<Tag> RecuperarTagConPostsRelacionados(string urlSlug)
        {
            return await Tags()
                .ConPostsPublicados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }

        public async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            var fechaPostLimiteSuperior = fechaPost.AddDays(1).AddSeconds(-1);
            return await Posts()
                            .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug && fechaPost <= m.FechaPost && m.FechaPost <= fechaPostLimiteSuperior);
        }


        public IQueryable<ArchivoItemDto> ConsultaDeArchivoBlog()
        {
            return Posts()
                .Publicados()
                .OrderByDescending(m => m.FechaPost)
                .GroupBy(p => new { p.FechaPost.Year, p.FechaPost.Month })
                .Select(g => new ArchivoItemDto
                {
                    Mes = g.Key.Month,
                    Anyo = g.Key.Year,
                    NumPosts = g.Count()
                });
        }



        public void Dispose()
        {
            _db.Dispose();
        }


    
    }
}
