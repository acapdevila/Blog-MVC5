using System.Linq;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Servicios;
using Blog.ViewModels.Menu;

namespace Blog.Smoothies.Controllers
{
    public class MenuController : Controller
    {
        private readonly CategoriasServicio _categoriasServicio;

        public MenuController() : this(new ContextoBaseDatos())
        {

        }

        public MenuController(ContextoBaseDatos bd) : this(new CategoriasServicio(bd, BlogController.TituloBlog))
        {

        }

        public MenuController(CategoriasServicio categoriasServicio)
        {
            _categoriasServicio = categoriasServicio;
            
        }


        [ChildActionOnly]
        public ActionResult Categorias()
        {
            var categorias = _categoriasServicio.CategoriasConPostsPublicados();

            var menuCategoriasViewModel = new MenuCategoriasViewModel(categorias);

            return View(menuCategoriasViewModel);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoriasServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }

   
}
