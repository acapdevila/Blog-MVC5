using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Smoothies.Controllers
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