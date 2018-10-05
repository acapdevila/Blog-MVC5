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
        //private readonly CacheService _cache = new CacheService();

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
            //MenuCategoriasViewModel viewModel = _cache.GetOrAdd(
            //    CacheSetting.PaginaPrincipal.Categorias, () =>
            //    {
                    var categorias = _categoriasServicio.Categorias().OrderBy(m=>m.Nombre).ToList();

                    var viewModel = new MenuCategoriasViewModel(categorias);
                //},
                //CacheSetting.PaginaPrincipal.SlidingExpiration);


            return View(viewModel);
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
