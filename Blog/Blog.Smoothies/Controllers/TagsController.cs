using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Tags;
using Blog.Servicios;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly TagsServicio _tagsServicio = new TagsServicio(new ContextoBaseDatos(), BlogController.TituloBlog);

        // GET: Tags
        public async Task<ActionResult> Index()
        {
            var viewModel = await _tagsServicio.ObtenerListaTagsViewModel();
            return View(viewModel);
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
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,UrlSlug")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagsServicio.CrearTag(tag);
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
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,UrlSlug")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagsServicio.ActualizarTag(tag);
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

        public ActionResult QuickMultipleSearch(string term)
        {
            var search = _tagsServicio.Tags()
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
                _tagsServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
