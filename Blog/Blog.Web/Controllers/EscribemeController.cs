using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class EscribemeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}