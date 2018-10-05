using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.Servicios.Cache;
using Blog.ViewModels.Blog;
using Blog.ViewModels.Sidebar;


namespace Blog.Web.Controllers
{
    public class SidebarController : Controller
    {
        private readonly TagsServicio _tagsServicio;
        private readonly BlogServicio _blogServicio;
        private readonly CacheService _cache = new CacheService();


        public SidebarController(): this(new ContextoBaseDatos())
        {
                
        }

        public SidebarController(ContextoBaseDatos bd) : this(new TagsServicio(bd, BlogController.TituloBlog), new BlogServicio(bd, BlogController.TituloBlog))
        {

        }

        public SidebarController(TagsServicio tagsServicio, BlogServicio blogServicio)
        {
            _tagsServicio = tagsServicio;
            _blogServicio = blogServicio;
        }

        [ChildActionOnly]
        public ActionResult NubeEtiquetas()
        {
            var etiquetas = _tagsServicio.Tags().OrderBy(m => m.Nombre).ToList();
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
        }

        [ChildActionOnly]
        public ActionResult ArchivoEtiquetas()
        {
            ArchivoEtiquetasViewModel viewModel = _cache.GetOrAdd(
                CacheSetting.PaginaPrincipal.Etiquetas, () =>
                {
                    var etiquetasArchivo = _blogServicio
                        .ConsultaDeArchivoBlog()
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

                    var itemsArchivo = _blogServicio
                        .Posts()
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
            var etiquetasArchivo = _blogServicio.RecuperarTodasLineasArchivoBlog()
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
                _tagsServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}
