using System.Web.Mvc;
using Ac.Web.Helpers;
using Ac.Web.ViewModels.Principal;

namespace Ac.Web.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public ActionResult AvisoLegal()
        {
            var viewmodel = new AvisoLegalViewmodel
            {
                PaginaWeb = WebConfigParametro.AvisoLegalPaginaWeb,
                Email = WebConfigParametro.AvisoLegalEmail,
                NombreLegal = WebConfigParametro.AvisoLegalNombreLegal,
                Nif = WebConfigParametro.AvisoLegalCifNif,
                Direccion = WebConfigParametro.AvisoLegalDireccion
            };

            return View(viewmodel);
        }
        public ActionResult PoliticaCookies()
        {
            var viewmodel = new PoliticaCookiesViewmodel
            {
                PaginaWeb = WebConfigParametro.AvisoLegalPaginaWeb
            };

            return View(viewmodel);
        }


    }
}