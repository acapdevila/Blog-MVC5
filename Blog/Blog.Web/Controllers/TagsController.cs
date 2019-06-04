using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Ac.Datos;
using Ac.Modelo.Tags;
using Blog.Servicios;
using Ac.ViewModels.Etiqueta;


namespace Blog.Web.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly TagsServicio _tagsServicio = new TagsServicio(new ContextoBaseDatos());

        // GET: Tags
        public async Task<ActionResult> Index()
        {
            var tags = await _tagsServicio.RecuperarListaTagsAsync();
            return View(tags);
        }

        // GET: Tags/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = await _tagsServicio.RecuperarTag(id.Value);
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
                await _tagsServicio.CrearTag(tag.ToDto());
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
            var tag = await _tagsServicio.RecuperarTag(id.Value);
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
        public async Task<ActionResult> Edit(EditarEtiquetaViewModel tag)
        {
            if (ModelState.IsValid)
            {
                await _tagsServicio.ActualizarTag(tag.ToDto());
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag =  await _tagsServicio.RecuperarTag(id.Value);
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
            await _tagsServicio.EliminarTag(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tagsServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
