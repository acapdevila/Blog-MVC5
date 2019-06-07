using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Ac.Datos;
using Ac.Modelo;
using Ac.Modelo.Dtos;
using Ac.Modelo.Posts;
using Ac.Web.ViewModels.Blog;
using Ac.Web.ViewModels.Etiqueta;
using Ac.Web.ViewModels.Sidebar;
using PagedList;
using PagedList.EntityFramework;

namespace Ac.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ContextoBaseDatos _db;
        private const int NumeroItemsPorPagina = 10;

        public BlogController()
        {
            _db  = new ContextoBaseDatos();
        }

        [OutputCache(Duration = 3600,  Location = OutputCacheLocation.Client, VaryByParam = "pagina", NoStore = true)]
        public async Task<ActionResult> Index(int? pagina)
        {
            var viewModel = await ObtenerListaPostsBlogViewModel(pagina ?? 1, NumeroItemsPorPagina);
            return View(viewModel);
        }
        

        public ActionResult Detalles(int dia, int mes, int anyo, string urlSlug)
        {
            return RedirectPermanent(@"/" + urlSlug);
        }

        public async Task<ActionResult> DetallesAmigable(string urlSlug)
        {
            Post post = await _db.Posts
                .Include(m => m.Tags)
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);

            if (post == null)
            {
                return HttpNotFound();
            }
            
            var postsSugeridos = await RecuperarPostsSugeridosViewmodel(post);


            var viewModel = new DetallesPostBlogViewModel
            {
                Post = post,
                PostsSugeridos = postsSugeridos
            };

            return View("Detalles", viewModel);

        }


        private DateTime? GenerarFecha(int dia, int mes, int anyo)
        {
            return new DateTime(anyo, mes, dia);
        }

        public ActionResult Etiqueta(string id, int pagina = 1)
        {
            if (1 < pagina)
                return RedirectPermanent(@"/" + id + @"?pagina=" + 1);

            return RedirectPermanent(@"/" + id);

        }

        public async Task<ActionResult> EtiquetaAmigable(string urlEtiqueta, int pagina = 1)
        {
            var etiqueta = await _db.Tags
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any())
                .FirstOrDefaultAsync(m => m.UrlSlug == urlEtiqueta);

            if (etiqueta == null) return HttpNotFound();

            var viewModel = new EtiquetaViewModel
            {
                Etiqueta = etiqueta,
                ListaPosts = etiqueta.Posts.AsQueryable()
                    .Publicados()
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .ToPagedList(pagina, NumeroItemsPorPagina)
            };


            return View("Etiqueta", viewModel);
        }



        public async Task<ActionResult> Archivo(int anyo, int mes)
        {
            var archivo = await RecuperarArchivoBlog(anyo, mes);

            if (archivo == null) return HttpNotFound();

            var viewModel = new ArchivoViewModel
            {
                ArchivoItem = new ArchivoItemViewModel(archivo),
                ListaPosts = _db.Posts
                                .Publicados()
                                .Where(m => m.FechaPost.Year == anyo && m.FechaPost.Month == mes)
                                .SeleccionaLineaResumenPost()
                                .OrderByDescending(m => m.FechaPost)
                                .ToList()
            };


            return View(viewModel);
        }
        
        private async Task<ArchivoItemDto> RecuperarArchivoBlog(int anyo, int mes)
        {
            return await _db.Posts
                    .Publicados()
                    .OrderByDescending(m => m.FechaPost)
                    .GroupBy(p => new { p.FechaPost.Year, p.FechaPost.Month })
                    .Select(g => new ArchivoItemDto
                    {
                        Mes = g.Key.Month,
                        Anyo = g.Key.Year,
                        NumPosts = g.Count()
                    })
                    .FirstOrDefaultAsync(m => m.Anyo == anyo && m.Mes == mes);
        }

        private async Task<ListaPostsBlogResumidosViewModel> ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {

            if (pagina < 1) pagina = 1;

            return new ListaPostsBlogResumidosViewModel
            {
                ListaPosts = await _db.Posts
                    .Publicados()
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .ToPagedListAsync(pagina, numeroItemsPorPagina)

            };

        }

        private async Task<List<LineaResumenPost>> RecuperarPostsAterioresMismoTag(Post post, int numPostAnteriores)
        {
            var postsAnterioresMismaCategoria =
                await _db.Posts
                    .Publicados()
                    .DeTags(post.Tags)
                    .Anteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .Take(numPostAnteriores)
                    .ToListAsync();


            return postsAnterioresMismaCategoria;
        }



        private async Task<List<LineaResumenPost>> RecuperarPostsPosterioresMismoTag(Post post, int numPostPosteriores)
        {
            var postsAnterioresMismTag =
                await _db.Posts
                    .Publicados()
                    .DeTags(post.Tags)
                    .Posteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderBy(m => m.FechaPost)
                    .Take(numPostPosteriores)
                    .ToListAsync();

            return postsAnterioresMismTag;
        }

        private async Task<List<LineaResumenPost>> RecuperarPostsSugeridosViewmodel(Post post)
        {
            List<LineaResumenPost> postsSugeridosAnteriores = await RecuperarPostsAterioresMismoTag(post, 3);
            List<LineaResumenPost> postsSugeridosPosteriores = await RecuperarPostsPosterioresMismoTag(post, 3);

            var postsSugeridos = postsSugeridosAnteriores.Union(postsSugeridosPosteriores).ToList();

            if (!postsSugeridos.Any())
            {
                postsSugeridos = await _db.Posts
                    .Publicados()
                    .Anteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .Take(6)
                    .ToListAsync();
            }

            return postsSugeridos;
        }

    }


}