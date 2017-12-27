using System;
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
using CSharpFunctionalExtensions;

namespace Blog.Smoothies.Controllers
{
    public class BlogController : Controller
    {
        public static string TituloBlog = "Smoothies de Cuchara";

        private readonly BlogServicio _blogServicio = new BlogServicio(new ContextoBaseDatos(), TituloBlog);
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

            CriteriosBusqueda critersosBusqueda = criteriosBusquedaOError.Value;

            var viewModel = ObtenerResultadosBusquedaViewModel(critersosBusqueda, pagina ?? 1, NumeroItemsPorPagina);
            return View("ResultadoBusqueda" ,viewModel);
        }



        public async Task<ActionResult> Detalles(string dia, string mes, string anyo, string urlSlug)
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
            return View(post);
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

        private async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await _blogServicio.RecuperarPost(fechaPost, urlSlug);
        }
    }

  
}