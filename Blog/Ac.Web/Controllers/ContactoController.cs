using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Ac.Web.Helpers;
using Ac.Web.Servicios.Contacto;
using Amazon.Runtime;

namespace Ac.Web.Controllers
{
    public class ContactoController : Controller
    {
        private readonly AmazonSesEmailSender _emailServicio;

        public ContactoController()
        {
            _emailServicio = new AmazonSesEmailSender(
                new BasicAWSCredentials(
                    WebConfigParametro.AwsAccessKey, 
                    WebConfigParametro.AwsSecretKey));
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormularioContactoViewModel viewmodel)
        {
            if (!ModelState.IsValid) return View(viewmodel);

            if (!viewmodel.EsCaptchaValido)
                return RedirectToAction("MensajeNoEnviado");

            var body =
                $"{viewmodel.Mensaje}\r\n\r\n\r\nNombre: {viewmodel.Nombre}\r\nE-mail: {viewmodel.Email}\r\nTeléfono:{viewmodel.Telefono}\r\n* Mensaje enviado desde el formulario de contacto del Blog";
            
            var respuesta = await _emailServicio.EnviarEmailDeContactoAsync(viewmodel.Nombre, viewmodel.Email, viewmodel.Asunto, body);
          
            //_emailServicio.EnviarFormularioContacto(viewmodel);
                
           
               
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