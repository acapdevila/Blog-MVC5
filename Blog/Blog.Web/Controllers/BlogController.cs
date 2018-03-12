﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.ViewModels.Blog;
using Blog.ViewModels.Etiqueta;
using Blog.ViewModels.Sidebar;
using PagedList;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public static string TituloBlog = "albertcapdevila.net";

        private readonly BlogServicio _blogServicio = new BlogServicio(new ContextoBaseDatos(), TituloBlog);
        private const int NumeroItemsPorPagina = 10;


        public ActionResult Index(int? pagina)
        {
            var viewModel = ObtenerListaPostsBlogViewModel(pagina ?? 1, NumeroItemsPorPagina);
            return View(viewModel);
        }
        

        public async Task<ActionResult> Detalles(int dia, int mes, int anyo, string urlSlug)
        {
            if (string.IsNullOrEmpty(urlSlug))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fechaPost = GenerarFecha(dia, mes, anyo);

            if(fechaPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = await RecuperarPost(fechaPost.Value, urlSlug);

            if (post == null)
            {
                return HttpNotFound();
            }

            List<LineaResumenPost> postsSugeridosAnteriores = await RecuperarPostsAterioresMismoTag(post, 2);
            List<LineaResumenPost> postsSugeridosPosteriores = await RecuperarPostsPosterioresMismoTag(post, 2);

            var postsSugeridos = postsSugeridosAnteriores.Union(postsSugeridosPosteriores).ToList();

            if (postsSugeridos.Any())
            {
                postsSugeridos = await _blogServicio.Posts()
                    .Publicados()
                    .Anteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .Take(4)
                    .ToListAsync();
            }

            var viewModel = new DetallesPostBlogViewModel
            {
                Post = post,
                PostsSugeridos = postsSugeridos
            };

            
            return View(viewModel);
        }

        private DateTime? GenerarFecha(int dia, int mes, int anyo)
        {
            return new DateTime(anyo, mes, dia);
        }

        public async Task<ActionResult> Etiqueta(string id, int pagina = 1)
        {
            var tag = await RecuperarTag(id);

            if (tag == null) return HttpNotFound();

            var viewModel = new EtiquetaViewModel
            {
                Id = id,
                NombreEtiqueta = tag.Nombre,
              ListaPosts = tag.Posts.AsQueryable()
                        .SeleccionaLineaResumenPost()
                        .OrderByDescending(m => m.FechaPost)
                        .ToPagedList(pagina, NumeroItemsPorPagina)
            };
                
                
                
            return View(viewModel);
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

        private async Task<Tag> RecuperarTag(string urlSlug)
        {
            return await _blogServicio.RecuperarTagConPostsRelacionados(urlSlug);
        }

        private async Task<ArchivoItemDto> RecuperarArchivoBlog(int anyo, int mes)
        {
            return await _blogServicio
                        .ConsultaDeArchivoBlog()
                        .FirstOrDefaultAsync(m => m.Anyo == anyo && m.Mes == mes);
        }

        private ListaPostsBlogResumidosViewModel ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {
            return new ListaPostsBlogResumidosViewModel
            {
                ListaPosts = _blogServicio.ObtenerListaResumenPostsPublicados(pagina, numeroItemsPorPagina)
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

    }


}