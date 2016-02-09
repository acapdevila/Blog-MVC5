using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Web.ViewModels.Blog;
using Blog.Web.ViewModels.Etiqueta;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ContextoBaseDatos _db = new ContextoBaseDatos();

        public async Task<ActionResult> Index()
        {
            var viewModel = await ObtenerListaPostsBlogViewModel();
            return View(viewModel);
        }

        private async Task<ListaPostsBlogViewModel> ObtenerListaPostsBlogViewModel()
        {
            return new ListaPostsBlogViewModel
            {
                ListaPosts = await _db.Posts
                .Publicados()
                .SeleccionaLineaResumePost()
                .OrderByDescending(m=>m.FechaPost)
                .ToListAsync()
            };
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

        public async Task<ActionResult> Etiqueta(string id)
        {
            var tag = await _db.Tags.Include(m => m.Posts)
                         .ConPostsPublicados()
                        .FirstOrDefaultAsync(m => m.UrlSlug == id);

            if (tag == null) return HttpNotFound();

            var viewModel = new EtiquetaViewModel
            {
              NombreEtiqueta = tag.Nombre,
              ListaPosts = tag.Posts.AsQueryable()
                        .SeleccionaLineaResumePost()
                        .OrderByDescending(m => m.FechaPost)
                        .ToList()
            };
                
                
                
            return View(viewModel);
        }

        private async Task<Post> RecuperarPost(DateTime fechaPost, string urlSlug)
        {
            return await _db.Posts.Include(m => m.Tags)
                            .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug && m.FechaPost == fechaPost);
        }
    }

  
}