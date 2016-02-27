﻿using System.Linq;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Tags;
using Blog.Web.ViewModels.Sidebar;


namespace Blog.Web.Controllers
{
    public class SidebarController : Controller
    {
        private readonly ContextoBaseDatos _db = new ContextoBaseDatos();

        [ChildActionOnly]
        public ActionResult NubeEtiquetas()
        {
            var etiquetas = _db.Tags.ConPostsPublicados().ToList();
            var nubeEtiquetasViewModel = new NubeEtiquetasViewModel(etiquetas);

            return View(nubeEtiquetasViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}
