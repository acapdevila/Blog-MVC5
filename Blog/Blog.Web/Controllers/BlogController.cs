using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Blog.Datos;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.Servicios.Cache;
using Blog.ViewModels.Blog;
using Blog.ViewModels.Etiqueta;
using Blog.ViewModels.Sidebar;
using PagedList;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public static string TituloBlog = "albertcapdevila.net";

        private readonly BlogServicio _blogServicio;
        private const int NumeroItemsPorPagina = 10;

        public BlogController()
        {
            var contexto = new ContextoBaseDatos();
            _blogServicio = new BlogServicio(contexto, TituloBlog);
        }

        [OutputCache(Duration = 3600,  Location = OutputCacheLocation.Client, VaryByParam = "pagina", NoStore = true)]
        public async Task<ActionResult> Index(int? pagina)
        {
            var viewModel = await ObtenerListaPostsBlogViewModel(pagina ?? 1, NumeroItemsPorPagina);
            return View(viewModel);
        }
        

        public async Task<ActionResult> Detalles(int dia, int mes, int anyo, string urlSlug)
        {
            if (string.IsNullOrEmpty(urlSlug))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fechaPost = GenerarFecha(dia, mes, anyo);

            if (fechaPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = await RecuperarPost(fechaPost.Value, urlSlug);

            if (post == null)
            {
                return HttpNotFound();
            }

            return RedirectPermanent(@"/" + urlSlug);
        }

        public async Task<ActionResult> DetallesAmigable(string urlSlug)
        {
            Post post = await _blogServicio.RecuperarPost(urlSlug);

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
            var etiqueta = await  _blogServicio.RecuperarTagConPostsRelacionados(urlEtiqueta);

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
                ListaPosts = _blogServicio
                                .Posts()
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
            return await _blogServicio
                        .ConsultaDeArchivoBlog()
                        .FirstOrDefaultAsync(m => m.Anyo == anyo && m.Mes == mes);
        }

        private async Task<ListaPostsBlogResumidosViewModel> ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {
            return new ListaPostsBlogResumidosViewModel
            {
                ListaPosts = await _blogServicio.ObtenerListaResumenPostsPublicados(pagina, numeroItemsPorPagina)
            };
        }

        private async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await _blogServicio.RecuperarPost(fechaPost, urlSlug);
        }


        private async Task<List<LineaResumenPost>> RecuperarPostsAterioresMismoTag(Post post, int numPostAnteriores)
        {
            var postsAnterioresMismaCategoria =
                await _blogServicio.Posts()
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
                await _blogServicio.Posts()
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
                postsSugeridos = await _blogServicio.Posts()
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