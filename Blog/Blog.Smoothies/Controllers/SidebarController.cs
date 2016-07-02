using System.Linq;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.ViewModels.Sidebar;

namespace Blog.Smoothies.Controllers
{
    public class SidebarController : Controller
    {
        private readonly TagsServicio _tagsServicio = new TagsServicio(new ContextoBaseDatos(), BlogController.TituloBlog);

        [ChildActionOnly]
        public ActionResult NubeEtiquetas()
        {
            var etiquetas = _tagsServicio.TagsConPostsPublicados();
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
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
