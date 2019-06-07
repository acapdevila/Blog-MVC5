using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Servicios;
using Blog.ViewModels.Categoria;

namespace LG.Web.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly CategoriasServicio _categoriasServicio;
        public CategoriasController()
        {
            var db = new ContextoBaseDatos();
            _categoriasServicio = new CategoriasServicio(db);
        }

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            var viewModel = await _categoriasServicio.ObtenerListaGestionCategorias(1, 200000);
            return View(viewModel);
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = await _categoriasServicio.RecuperarCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View(new EditarCategoriaViewModel(new Categoria()));
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EditarCategoriaViewModel categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriasServicio.CrearCategoria(categoria.ToDto());
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = await _categoriasServicio.RecuperarCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(new EditarCategoriaViewModel(categoria));
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditarCategoriaViewModel categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriasServicio.ActualizarCategoria(categoria.ToDto());
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria =  await _categoriasServicio.RecuperarCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _categoriasServicio.EliminarCategoria(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult QuickMultipleSearch(string term)
        {
            var search = _categoriasServicio.Categorias()
                            .Where(m => m.Nombre.Contains(term))
                            .OrderBy(m => m.Nombre)
                            .Take(10)
                            .Select(m => new { value = m.Nombre, id = m.Id }).ToList();
            ;
            return Json(search, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoriasServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
