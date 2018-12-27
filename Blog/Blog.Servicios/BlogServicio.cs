using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using PagedList;
using PagedList.EntityFramework;

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
                .Include(m=>m.Categorias)
                .Where(m => m.Blog.Titulo == _tituloBlog);
        }

        private IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any(p=>p.Blog.Titulo == _tituloBlog));
        }
        private IQueryable<Categoria> Categorias()
        {
            return _db.Categorias
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));
        }


        public async Task<IPagedList<LineaResumenPost>> ObtenerListaResumenPostsPublicados(int pagina, int nummeroItemsPorPagina)
        {
            if (pagina < 1) pagina = 1;

            return await Posts()
                .Publicados()
                .SeleccionaLineaResumenPost()
                .OrderByDescending(m => m.FechaPost)
                .ToPagedListAsync(pagina, nummeroItemsPorPagina);
            
        }

        public async Task<IPagedList<LineaPostCompleto>> ObtenerListaPostsCompletosPublicados(int pagina, int nummeroItemsPorPagina)
        {
            var  postsProyectados = await Posts()
                    .Publicados()
                .Select(m => new 
                {
                   m.Id,
                  m.UrlSlug,
                  m.Titulo,
                  m.Subtitulo,
                   m.FechaPost,
                  m.Autor,
                  m.ContenidoHtml
                })
                .OrderByDescending(m => m.FechaPost)
                .ToPagedListAsync(pagina, nummeroItemsPorPagina);



            return new StaticPagedList<LineaPostCompleto>(postsProyectados.Select(m => new LineaPostCompleto
            {
                Id = m.Id,
                UrlSlug = m.UrlSlug,
                Titulo = m.Titulo,
                Subtitulo = m.Subtitulo,
                FechaPost = m.FechaPost,
                Autor = m.Autor,
                ContenidoHtml = m.ContenidoHtml
            }), postsProyectados.PageNumber, postsProyectados.PageSize, postsProyectados.TotalItemCount);
        }

        public async Task<IPagedList<LineaPostCompleto>> BuscarPostsCompletosPublicados(
            CriteriosBusqueda criteriosBusqueda, 
            int pagina, 
            int nummeroItemsPorPagina)
        {
            var tags = await Tags().Where(t=>criteriosBusqueda.PalabrasBuscadas.Contains(t.Nombre)).ToListAsync();
            var categorias = await Categorias().Where(c => criteriosBusqueda.PalabrasBuscadas.Any(p => p.Contains(c.Nombre) || c.Nombre.Contains(p))).ToListAsync();
            
            var postsProyectados = await Posts()
                .Publicados()
                .BuscarPor(criteriosBusqueda, tags, categorias)
                .OrderByDescending(m => m.FechaPost)
                .Select(m => new
                {
                    m.Id,
                    m.UrlSlug,
                    m.Titulo,
                    m.Subtitulo,
                    m.FechaPost,
                    m.Autor,
                    m.ContenidoHtml
                })
                .ToPagedListAsync(pagina, nummeroItemsPorPagina);

                return new StaticPagedList<LineaPostCompleto>(postsProyectados.Select(m => new LineaPostCompleto
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    Subtitulo = m.Subtitulo,
                    FechaPost = m.FechaPost,
                    Autor = m.Autor,
                    ContenidoHtml = m.ContenidoHtml
                }), postsProyectados.PageNumber, postsProyectados.PageSize, postsProyectados.Count);

        }

        public async Task<Tag> RecuperarTagConPostsRelacionados(string urlSlug)
        {
            return await Tags()
              //  .ConPostsPublicados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }

        public async Task<Categoria> RecuperarCategoriaConPostsRelacionados(string urlSlug)
        {
            return await Categorias()
              //  .ConPostsPublicados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }

        public async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            var fechaPostLimiteSuperior = fechaPost.AddDays(1).AddSeconds(-1);
            return await Posts()
                            .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug && fechaPost <= m.FechaPost && m.FechaPost <= fechaPostLimiteSuperior);
        }


        public async Task<Post> RecuperarPost(string urlSlug)
        {
            return await Posts()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }

        public async Task<BlogEntidad> RecuperarBlog()
        {
            return await _db.Blogs
                .FirstOrDefaultAsync(m => m.Titulo == _tituloBlog);
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



        public List<LineaArchivoDto> RecuperarTodasLineasArchivoBlog()
        {
            return Posts()
                .Publicados()
                .OrdenadosPorFecha()
                .Select(g => new LineaArchivoDto
                {
                    FechaPost = g.FechaPost,
                    Titulo = g.Titulo,
                    UrlSlug = g.UrlSlug
                })
                .ToList();
        }


        public void Dispose()
        {
            _db.Dispose();
        }


       
    }
}
