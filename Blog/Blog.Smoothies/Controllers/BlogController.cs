﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.Servicios.Cache;
using Blog.ViewModels.Blog;
using Blog.ViewModels.Etiqueta;
using Blog.ViewModels.Sidebar;
using PagedList;
using CSharpFunctionalExtensions;
using Blog.ViewModels.Categoria;

namespace Blog.Smoothies.Controllers
{
    public class BlogController : Controller
    {
        public static string TituloBlog = "by Laura García";

        private readonly BlogServicio _blogServicio;
        private readonly TagsServicio _tagsServicio;
        
        public BlogController()
        {
            var contexto = new ContextoBaseDatos();
            _blogServicio = new BlogServicio(contexto, TituloBlog);
            _tagsServicio = new TagsServicio(contexto, TituloBlog);

        }

        private const int NumeroItemsPorPagina = 11;

        public async Task<ActionResult> Index(int? pagina)
        {
            var viewModel = await ObtenerListaPostsBlogViewModel(pagina ?? 1 ,NumeroItemsPorPagina);
            return View(viewModel);
        }


        public async  Task<ActionResult> Buscar(string buscarPor,int? pagina)
        {
            Result<CriteriosBusqueda> criteriosBusquedaOError = CriteriosBusqueda.Crear(buscarPor);

            if (criteriosBusquedaOError.IsFailure)
                return RedirectToAction("Index");

            CriteriosBusqueda criteriosBusqueda = criteriosBusquedaOError.Value;

            List<Tag> tags = _tagsServicio.BuscarTags(criteriosBusqueda);

            criteriosBusqueda.AñadirTags(tags);

            var viewModel = await ObtenerResultadosBusquedaViewModel(criteriosBusqueda, pagina ?? 1, NumeroItemsPorPagina);
            return View("ResultadoBusqueda" ,viewModel);
        }



        public async Task<ActionResult> Detalles(string dia, string mes, string anyo, string urlSlug)
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

            return View("Detalles",viewModel);

        }

        private DateTime? GenerarFecha(string dia, string mes, string anyo)
        {
            int numDia;
            if (!int.TryParse(dia, out numDia)) return null;

            int numMes;
            if (!int.TryParse(mes, out numMes)) return null;

            int numAnyo;
            if (!int.TryParse(anyo, out numAnyo)) return null;

            return new DateTime(numAnyo, numMes, numDia);

        }

        public async Task<ActionResult> Etiqueta(string id, int pagina = 1)
        {
            var tag = await RecuperarTag(id);

            if (tag == null) return HttpNotFound();

            var viewModel = new EtiquetaViewModel
            {
                Etiqueta =  tag,
              ListaPosts = tag.Posts.AsQueryable()
                        .SeleccionaLineaResumenPost()
                        .OrderByDescending(m => m.FechaPost)
                        .ToPagedList(pagina, NumeroItemsPorPagina)
            };
                
                
                
            return View(viewModel);
        }

        public ActionResult Categoria(string id, int pagina = 1)
        {
            if(1 < pagina)
                return RedirectPermanent(@"/" + id + @"?pagina=" + 1);

            return RedirectPermanent(@"/" + id);

        }

        public async Task<ActionResult> CategoriaAmigable(string urlCategoria, int pagina = 1)
        {
            var categoria = await RecuperarCategoria(urlCategoria);

            if (categoria == null) return HttpNotFound();

            var viewModel = new CategoriaViewModel
            {
                Categoria = categoria,
                ListaPosts = categoria.Posts.AsQueryable()
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .ToPagedList(pagina, NumeroItemsPorPagina)
            };
            

            return View("Categoria", viewModel);
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
                                .Where(m=>m.FechaPost.Year == anyo && m.FechaPost.Month == mes)
                                .SeleccionaLineaResumenPost()
                                .OrderByDescending(m => m.FechaPost)
                                .ToList()    
            };
            

            return View(viewModel);
        }

        private async Task<Tag> RecuperarTag(string urlSlug)
        {
            return await _blogServicio.RecuperarTagConPostsRelacionados(urlSlug);
        }
        private async Task<Categoria> RecuperarCategoria(string urlSlug)
        {
            return await _blogServicio.RecuperarCategoriaConPostsRelacionados(urlSlug);
        }

        private async Task<ArchivoItemDto> RecuperarArchivoBlog(int anyo, int mes)
        {
            return await _blogServicio
                        .ConsultaDeArchivoBlog()
                        .FirstOrDefaultAsync(m=>m.Anyo == anyo && m.Mes == mes);
        }

        private async Task<ListaPostsBlogCompletosViewModel> ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {
            //ListaPostsBlogCompletosViewModel listaPostViewmodel = await _cache.GetOrAdd(
            //    CacheSetting.PaginaPrincipal.Posts, async () =>
            //    {
                    return new ListaPostsBlogCompletosViewModel
                    {
                        ListaPosts = await _blogServicio.ObtenerListaPostsCompletosPublicados(pagina, numeroItemsPorPagina)
                    };
                //},
                //CacheSetting.PaginaPrincipal.SlidingExpiration);

            //return listaPostViewmodel;

        }

        private async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await _blogServicio.RecuperarPost(fechaPost, urlSlug);
        }


        private async Task<ResultadoBusquedaViewModel> ObtenerResultadosBusquedaViewModel(CriteriosBusqueda buscarPor,int pagina, int numeroItemsPorPagina)
        {

            return new ResultadoBusquedaViewModel
            {
                BuscarPor = buscarPor,
                ListaPosts = await _blogServicio.BuscarPostsCompletosPublicados(buscarPor, pagina, numeroItemsPorPagina)
            };
        }

        private async Task<List<LineaResumenPost>> RecuperarPostsAterioresMismaCategoria(Post post, int numPostAnteriores)
        {
            var postsAnterioresMismaCategoria =
                await _blogServicio.Posts()
                    .Publicados()
                    .DeCategorias(post.Categorias)
                    .Anteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .Take(numPostAnteriores)
                    .ToListAsync();


            return postsAnterioresMismaCategoria;
        }



        private async Task<List<LineaResumenPost>> RecuperarPostsPosterioresMismaCategoria(Post post, int numPostPosteriores)
        {
            var postsAnterioresMismaCategoria =
                await _blogServicio.Posts()
                    .Publicados()
                    .DeCategorias(post.Categorias)
                    .Posteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderBy(m => m.FechaPost)
                    .Take(numPostPosteriores)
                    .ToListAsync();

            return postsAnterioresMismaCategoria;
        }



        private async Task<List<LineaResumenPost>> RecuperarPostsSugeridosViewmodel(Post post)
        {
            List<LineaResumenPost> postsSugeridosAnteriores = await RecuperarPostsAterioresMismaCategoria(post, 3);
            List<LineaResumenPost> postsSugeridosPosteriores = await RecuperarPostsPosterioresMismaCategoria(post, 3);

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