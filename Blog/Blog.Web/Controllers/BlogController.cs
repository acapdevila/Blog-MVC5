using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Web.ViewModels.Blog;

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
                .Where(m=>!m.EsBorrador && m.FechaPublicacion <= DateTime.Now)
                .Select(m => new LineaResumenPost
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    Subtitulo = m.Subtitulo,
                    FechaPost = m.FechaPost,
                    Autor = m.Autor
                }).ToListAsync()
            };
        }

        public ActionResult Etiqueta(string id)
        {
            return View();
        }
    }
}