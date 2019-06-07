using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using PagedList;
using PagedList.EntityFramework;

namespace LG.Web.Servicios
{
    public class BlogServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly TagsServicio _tagsServicio;
        private readonly CategoriasServicio _categoriasServicio;
        public BlogServicio(ContextoBaseDatos db) : this(db, new TagsServicio(db), new CategoriasServicio(db))
        {
            
        }


        public BlogServicio(ContextoBaseDatos db, TagsServicio tagsServicio, CategoriasServicio categoriasServicio)
        {
            _db = db;
            _tagsServicio = tagsServicio;
            _categoriasServicio = categoriasServicio;
         }

        public IQueryable<Post> Posts()
        {
            return _db.Posts
                    .Include(m=>m.Receta.Ingredientes.Select(i => i.Ingrediente))
                .Include(m=>m.Receta.Instrucciones)
                .Include(m => m.Tags)
                .Include(m=>m.Categorias);
        }

        public IQueryable<Post> PostsYRelacionados()
        {
            return _db.Posts
                    .Include(m => m.Receta.Ingredientes.Select(i => i.Ingrediente))
                    .Include(m => m.Receta.Instrucciones)
                    .Include(m => m.Tags)
                    .Include(m => m.Categorias)
                    .Include(m => m.Utensilios.Select(u=>u.Utensilio))
                    .Include(m => m.PostRelacionados.Select(r => r.Hijo))
                    ;
            
            
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
            var tags = await _tagsServicio.BuscarTags(criteriosBusqueda.PalabrasBuscadas);
            var categorias = await _categoriasServicio.BuscarCategoriasAsync(criteriosBusqueda.PalabrasBuscadas); 
            
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
            return await _tagsServicio.RecuperarTagIncluyendoPostsPorUrlAsync(urlSlug);
        }

   

        public async Task<Categoria> RecuperarCategoriaConPostsRelacionados(string urlCategoria)
        {
            return await _categoriasServicio.RecuperarCategoriaIncluyendoPostsPorUrlCategoriaAsync(urlCategoria);
        }

        public async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            var fechaPostLimiteSuperior = fechaPost.AddDays(1).AddSeconds(-1);
            return await Posts()
                            .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug && fechaPost <= m.FechaPost && m.FechaPost <= fechaPostLimiteSuperior);
        }


        public async Task<Post> RecuperarPost(string urlSlug)
        {
            return await PostsYRelacionados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
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
            _tagsServicio.Dispose();
            _db.Dispose();
        }


        public async Task<List<Tag>> RecuperarListaTagsAsync()
        {
            return await _tagsServicio.RecuperarListaTagsAsync();
        }

        public List<Tag> RecuperarListaTags()
        {
            return _tagsServicio.RecuperarListaTags();
        }


    }
}
