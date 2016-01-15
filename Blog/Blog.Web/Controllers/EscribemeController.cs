using System.Web.Mvc;
using Blog.Web.Servicios;
using Blog.Web.ViewModels.Escribeme;

namespace Blog.Web.Controllers
{
    public class EscribemeController : Controller
    {
        private readonly IEmailServicio _emailServicio;

        public EscribemeController(): this(new EmailServicio())
        {
            
        }

        public EscribemeController(IEmailServicio emailServicio)
        {
            _emailServicio = emailServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormularioContactoViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
               _emailServicio.EnviarFormularioContacto(viewmodel);
                
                return RedirectToAction("MensajeEnviado");
            }

            return View(viewmodel);

        }

        public ActionResult MensajeEnviado()
        {
            return View();
        }
    }
}