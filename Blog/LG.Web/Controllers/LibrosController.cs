using System.Web.Mvc;

namespace LG.Web.Controllers
{
    public class LibrosController : Controller
    {
        // GET: Libros
        public ActionResult Index()
        {
            return View();
        }
    }
}