using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class HolaController : Controller
    {
        public ActionResult Index()
        {
            return View("MiNombreEsAlbert");
        }

        public ActionResult MiNombreEsAlbert()
        {
            return View();
        }
    }
}