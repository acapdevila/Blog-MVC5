using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.ViewModels.Blog;
using Blog.ViewModels.Etiqueta;
using PagedList;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public static string TituloBlog = "albertcapdevila.net";

        private readonly BlogServicio _db = new BlogServicio(new ContextoBaseDatos(), TituloBlog);
        private const int NumeroItemsPorPagina = 10;


        public ActionResult Index(int? pagina)
        {
            var viewModel = ObtenerListaPostsBlogViewModel(pagina ?? 1 ,NumeroItemsPorPagina);
            return View(viewModel);
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

        public async Task<ActionResult> Etiqueta(string id, int numeroPagina = 1)
        {
            var tag = await RecuperarTag(id);

            if (tag == null) return HttpNotFound();

            var viewModel = new EtiquetaViewModel
            {
              NombreEtiqueta = tag.Nombre,
              ListaPosts = tag.Posts.AsQueryable()
                        .SeleccionaLineaResumenPost()
                        .OrderByDescending(m => m.FechaPost)
                        .ToPagedList(numeroPagina, NumeroItemsPorPagina)
            };
                
                
                
            return View(viewModel);
        }

        private async Task<Tag> RecuperarTag(string urlSlug)
        {
            return await _db.RecuperarTagConPostsRelacionados(urlSlug);
        }

        private ListaPostsBlogViewModel ObtenerListaPostsBlogViewModel(int pagina, int numeroItemsPorPagina)
        {
            return _db.ObtenerListaPostsBlogViewModel(pagina, numeroItemsPorPagina);
        }

        private async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await _db.RecuperarPost(fechaPost, urlSlug);
        }
    }

  
}