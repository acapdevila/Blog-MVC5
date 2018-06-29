using System.Web.Mvc;

namespace Blog.Smoothies.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult InternalError()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}