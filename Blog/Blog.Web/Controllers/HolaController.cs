using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class HolaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}