using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Ac.Datos;
using Ac.Modelo;
using Ac.Modelo.Posts;

namespace Ac.Web.Controllers
{
    public class PortadaController : Controller
    {
        public class PortadaViewModel
        {
            public List<LineaResumenPost> UltimosPosts { get; set; } 
        }

        private readonly ContextoBaseDatos _db;
        public PortadaController()
        {
            _db = new ContextoBaseDatos();
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public async Task<ActionResult> Index()   
        {
            var viewModel = new PortadaViewModel
            {
                UltimosPosts = await RecuperarPostPortada()
            };
            return View(viewModel);
        }

        private async Task<List<LineaResumenPost>> RecuperarPostPortada()
        {
            return await Posts()
                .Publicados()
                .Where(m => !m.Titulo.Contains("lecturas recomendadas"))
                .SeleccionaLineaResumenPost()
                .OrderByDescending(m => m.FechaPost)
                .Take(3)
                .ToListAsync();
        }

        public IQueryable<Post> Posts()
        {
            return _db.Posts;
        }
    }
}