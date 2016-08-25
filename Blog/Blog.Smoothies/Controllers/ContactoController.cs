﻿using System.Web.Mvc;
using Blog.Servicios;
using Blog.ViewModels.Contacto;

namespace Blog.Smoothies.Controllers
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
            if (ModelState.IsValid)
            {
               _emailServicio.EnviarFormularioContacto(viewmodel);
                
                if(viewmodel.EsCaptchaValido)
                    return RedirectToAction("MensajeEnviado");
               
                return RedirectToAction("MensajeNoEnviado");
            }

            return View(viewmodel);

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