using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ViewModels.Libros;

namespace Blog.Web.Controllers
{
    public class LibrosProgramacionController : Controller
    {
        // GET: Libros
        public ActionResult Index()
        {
            var viewMmodel = new LibrosProgramacionViewmodel
            {
                Libros = new List<LibroViewmodel>
                {
                    new LibroViewmodel{ Titulo = "Joel spolsky"},
                    new LibroViewmodel{ Titulo = "Clean code, etc"},
                    new LibroViewmodel{ Titulo = "Patrones de diseño" },
                    new LibroViewmodel{ Titulo = "C# in depth" }

                }
            };
            return View();
        }
    }

    
}