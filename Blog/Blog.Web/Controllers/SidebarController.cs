using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo;
using Blog.Web.ViewModels.Sidebar;


namespace Blog.Web.Controllers
{
    public class SidebarController : Controller
    {
        private readonly ContextoBaseDatos db = new ContextoBaseDatos();

        [ChildActionOnly]
        public ActionResult NubeEtiquetas()
        {
            var etiquetas = db.Tags.ToList();
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}
