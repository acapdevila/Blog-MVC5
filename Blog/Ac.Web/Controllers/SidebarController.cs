using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ac.Datos;
using Ac.Modelo;
using Ac.Modelo.Dtos;
using Ac.Modelo.Tags;
using Ac.Web.ViewModels.Sidebar;
using Infra.Cache;

namespace Ac.Web.Controllers
{
    public class SidebarController : Controller
    {
        private readonly ContextoBaseDatos _db = new ContextoBaseDatos();
        private readonly CacheService _cache = new CacheService();


        [ChildActionOnly]
        public ActionResult NubeEtiquetas()
        {
            var etiquetas = Tags().OrderBy(m => m.Nombre).ToList();
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
        }

        [ChildActionOnly]
        public ActionResult ArchivoEtiquetas()
        {
            ArchivoEtiquetasViewModel viewModel = _cache.GetOrAdd(
                CacheSetting.PaginaPrincipal.Etiquetas, () =>
                {
                    var etiquetasArchivo = 
                        ConsultaDeArchivoBlog()
                        .ToList()
                        .Select(m => new ArchivoItemViewModel(m))
                        .OrderByDescending(m => m.Anyo)
                        .ThenByDescending(m => m.Mes)
                        .ToList();

                    return new ArchivoEtiquetasViewModel(etiquetasArchivo);
                },
                CacheSetting.PaginaPrincipal.SlidingExpiration);
            

            return View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult ArchivoArbol()
        {
            ArchivoArbolViewModel viewModel = _cache.GetOrAdd(
                CacheSetting.PaginaPrincipal.Archivo, () =>
                {

                    var itemsArchivo = _db.Posts
                        .Publicados()
                        .OrderByDescending(m => m.FechaPost)
                        .Select(m => new ItemArchivoArbolViewModel
                        {
                            FechaPost = m.FechaPost,
                            Titulo = m.Titulo,
                            Url = m.UrlSlug,
                        })
                        .ToList();

                    return new ArchivoArbolViewModel(itemsArchivo);
                },
                CacheSetting.PaginaPrincipal.SlidingExpiration);

            return View(viewModel);


        }


      

        [ChildActionOnly]
        public ActionResult ArchivoEnModoArbol()
        {
            var etiquetasArchivo = RecuperarTodasLineasArchivoBlog()
                                    .Select(m => new ArchivoItemViewModel(m))
                                    .OrderByDescending(m => m.Anyo)
                                    .ThenByDescending(m => m.Mes)
                                    .ToList();

            var nubeEtiquetasViewModel = new ArchivoEtiquetasViewModel(etiquetasArchivo);

            return Content(string.Empty); //  View(nubeEtiquetasViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Where(m => m.Posts.Any());
        }

        private IQueryable<ArchivoItemDto> ConsultaDeArchivoBlog()
        {
            return  _db.Posts
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

        private List<LineaArchivoDto> RecuperarTodasLineasArchivoBlog()
        {
            return _db.Posts
                .Publicados()
                .OrdenadosPorFecha()
                .Select(g => new LineaArchivoDto
                {
                    FechaPost = g.FechaPost,
                    Titulo = g.Titulo,
                    UrlSlug = g.UrlSlug
                })
                .ToList();
        }
    }

   
}
