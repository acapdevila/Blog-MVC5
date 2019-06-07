using System.Web.Mvc;
using Blog.Servicios;
using LG.Web.Servicios.Contacto;

namespace LG.Web.Controllers
{
    public class ContactoController : Controller
    {
        private readonly IEmailServicio _emailServicio;

        public ContactoController(): this(new EmailServicio())
        {
            
        }

        public ContactoController(IEmailServicio emailServicio)
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
            if (!ModelState.IsValid) return View(viewmodel);

            if (!viewmodel.EsCaptchaValido)
                return RedirectToAction("MensajeNoEnviado");
            
            _emailServicio.EnviarFormularioContacto(viewmodel);
            return RedirectToAction("MensajeEnviado");

        }

        public ActionResult MensajeEnviado()
        {
            return View();
        }
        public ActionResult MensajeNoEnviado()
        {
            return View();
        }
    }
}