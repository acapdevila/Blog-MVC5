using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ac.Datos;
using Ac.Modelo.Tags;
using Ac.Web.ViewModels.Etiqueta;

namespace Ac.Web.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ContextoBaseDatos _db = new ContextoBaseDatos();

        // GET: Tags
        public async Task<ActionResult> Index()
        {
            var tags = await Tags()
                .OrderBy(m => m.Nombre)
                .ToListAsync(); 

            return View(tags);
        }

        // GET: Tags/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = await Tags().FirstAsync(m => m.Id == id);

            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View(new EditarEtiquetaViewModel(new Tag()));
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EditarEtiquetaViewModel tag)
        {
            if (ModelState.IsValid)
            {
                var etiqueta = new Tag();
                etiqueta.CopiarValores(tag.ToDto());
                _db.Tags.Add(etiqueta);
                await _db.GuardarCambios();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = await _db.Tags.FirstOrDefaultAsync(m=>m.Id == id.Value);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(new EditarEtiquetaViewModel(tag));
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditarEtiquetaViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var tag = await _db.Tags.FirstAsync(m=>m.Id == viewmodel.Id);
                tag.CopiarValores(viewmodel.ToDto());
                await _db.GuardarCambios();
                return RedirectToAction("Index");
            }
            return View(viewmodel);
        }

        // GET: Tags/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag =  await _db.Tags.FirstOrDefaultAsync(m=>m.Id == id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var tag = await _db.Tags.FirstAsync(m => m.Id == id);
            _db.Tags.Remove(tag);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Where(m => m.Posts.Any());
        }
    }
}
