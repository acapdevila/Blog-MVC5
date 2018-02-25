using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
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
        public static string TituloBlog = "Smoothies de Cuchara";

        private readonly BlogServicio _blogServicio;
        private readonly TagsServicio _tagsServicio;
        
        public BlogController()
        {
            var contexto = new ContextoBaseDatos();
            _blogServicio = new BlogServicio(contexto, TituloBlog);
            _tagsServicio = new TagsServicio(contexto, TituloBlog);

        }

        private const int NumeroItemsPorPagina = 11;




        public ActionResult Index(int? pagina)
        {
            var viewModel = ObtenerListaPostsBlogViewModel(pagina ?? 1 ,NumeroItemsPorPagina);
            return View(viewModel);
        }


        public ActionResult Buscar(string buscarPor,int? pagina)
        {
            Result<CriteriosBusqueda> criteriosBusquedaOError = CriteriosBusqueda.Crear(buscarPor);

            if (criteriosBusquedaOError.IsFailure)
                return RedirectToAction("Index");

            CriteriosBusqueda criteriosBusqueda = criteriosBusquedaOError.Value;

            List<Tag> tags = _tagsServicio.BuscarTags(criteriosBusqueda);

            criteriosBusqueda.AñadirTags(tags);

            var viewModel = ObtenerResultadosBusquedaViewModel(criteriosBusqueda, pagina ?? 1, NumeroItemsPorPagina);
            return View("ResultadoBusqueda" ,viewModel);
        }



        public async Task<ActionResult> Detalles(string dia, string mes, string anyo, string urlSlug)
        {
            if (string.IsNullOrEmpty(urlSlug))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fechaPost = GenerarFecha(dia, mes, anyo);

            if(fechaPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Post post = await _blogServicio.RecuperarPost(fechaPost.Value, urlSlug);

            List<LineaResumenPost> postsSugeridosAnteriores = await RecuperarPostsAterioresMismaCategoria(post, 3);
            List<LineaResumenPost> postsSugeridosPosteriores = await RecuperarPostsPosterioresMismaCategoria(post, 3);

            var postsSugeridos = postsSugeridosAnteriores.Union(postsSugeridosPosteriores).ToList();
            
            if (postsSugeridos.Any())
            {
                postsSugeridos = await _blogServicio.Posts()
                    .Publicados()
                    .Anteriores(post)
                    .SeleccionaLineaResumenPost()
                    .OrderByDescending(m => m.FechaPost)
                    .Take(6)
                    .ToListAsync();
            }


            var viewModel = new DetallesPostBlogViewModel
            {
                Post = post,
                PostsSugeridos = postsSugeridos
            };

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
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
                Id =  id,
              NombreEtiqueta = tag.Nombre,
              ListaPosts = tag.Posts.AsQueryable()
                        .SeleccionaLineaResumenPost()
                        .OrderByDescending(m => m.FechaPost)
                        .ToPagedList(pagina, NumeroItemsPorPagina)
            };
                
                
                
            return View(viewModel);
        }

        public async Task<ActionResult> Categoria(string id, int pagina = 1)
        {
            var categoria = await RecuperarCategoria(id);

            if (categoria == null) return HttpNotFound();

            var viewModel = new CategoriaViewModel
            {
                Id = id,
                NombreCategoria = categoria.Nombre,
                ListaPosts = categoria.Posts.AsQueryable()
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

        private ListaPostsBlogCompletosViewModel ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {
            return new ListaPostsBlogCompletosViewModel
            {
                ListaPosts = _blogServicio.ObtenerListaPostsCompletosPublicados(pagina, numeroItemsPorPagina)
            };
        }


        private ResultadoBusquedaViewModel ObtenerResultadosBusquedaViewModel(CriteriosBusqueda buscarPor,int pagina, int numeroItemsPorPagina)
        {

            return new ResultadoBusquedaViewModel
            {
                BuscarPor = buscarPor,
                ListaPosts = _blogServicio.BuscarPostsCompletosPublicados(buscarPor, pagina, numeroItemsPorPagina)
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



    }


}