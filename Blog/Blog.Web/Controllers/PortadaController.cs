using System.Web.Mvc;
using System.Web.UI;

namespace Blog.Web.Controllers
{
    public class PortadaController : Controller
    {
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public ActionResult Index()   
        {
            return View();
        }

        
    }
}