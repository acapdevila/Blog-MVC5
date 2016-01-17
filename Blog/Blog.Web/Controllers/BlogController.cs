using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Etiqueta(string id)
        {
            return View();
        }
    }
}