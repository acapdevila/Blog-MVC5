using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.Servicios.Cache;
using Blog.ViewModels.Sidebar;

namespace Blog.Smoothies.Controllers
{
    public class SidebarController : Controller
    {
        private readonly BlogServicio _blogServicio;

        public SidebarController()
        {
            var contexto = new ContextoBaseDatos();
            _blogServicio = new BlogServicio(contexto, BlogController.TituloBlog);
        }


        [ChildActionOnly]
        public async Task<ActionResult> NubeEtiquetas()
        {
            var etiquetas = await _blogServicio.RecuperarListaTagsAsync(); 
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
        }

        [ChildActionOnly]
        public ActionResult ArchivoEtiquetas()
        {

            //ArchivoEtiquetasViewModel viewModel = _cache.GetOrAdd(
            //    CacheSetting.PaginaPrincipal.Etiquetas, () =>
            //    {
                    var etiquetasArchivo = _blogServicio
                        .ConsultaDeArchivoBlog()
                        .ToList()
                        .Select(m => new ArchivoItemViewModel(m))
                        .OrderByDescending(m => m.Anyo)
                        .ThenByDescending(m => m.Mes)
                        .ToList();

            var  viewModel = new ArchivoEtiquetasViewModel(etiquetasArchivo);
                //},
                //CacheSetting.PaginaPrincipal.SlidingExpiration);


            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _blogServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}
