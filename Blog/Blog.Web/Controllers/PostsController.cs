using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Datos.Repositorios;
using Blog.Modelo;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Web.ViewModels.Post;
using Blog.Web.ViewModels.Post.Conversores;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ContextoBaseDatos _db;
        private readonly AsignadorTags _asignadorTags;

        public PostsController() : this(new ContextoBaseDatos())
        {
            
        }

        public PostsController(ContextoBaseDatos db): this(db, new AsignadorTags(new TagRepositorio(db)))
        {

        }


        public PostsController(ContextoBaseDatos db, AsignadorTags asignadorTags)
        {
            _asignadorTags = asignadorTags;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = await ObtenerListaPostViewModel();
            return View(viewModel);
        }

        private async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel()
        {
            return new ListaGestionPostsViewModel
            {
                ListaPosts = await _db.Posts.Select(m => new LineaGestionPost
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Titulo = m.Titulo,
                    FechaPost = m.FechaPost,
                    EsBorrador = m.EsBorrador,
                    EsRssAtom = m.EsRssAtom,
                    FechaPublicacion = m.FechaPublicacion,
                    Autor = m.Autor, 
                    ListaTags = m.Tags
                })
                .OrderByDescending(m=>m.Id)
                .ToListAsync()
            };
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await RecuperarPost(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        
        public ActionResult Create()
        {
            var post = Post.CrearNuevoPorDefecto();

            var viewModel = new EditViewModel
            {
                EditorPost = new EditorPost()
            };

            viewModel.EditorPost.CopiaValores(post);
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await CrearPost(viewModel.EditorPost);
              
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
        

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await RecuperarPost(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditViewModel
            {
                EditorPost = new EditorPost()
            };

            viewModel.EditorPost.CopiaValores(post);
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarPost(viewModel.EditorPost);
              
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await RecuperarPost(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await EliminarPost(id);
            return RedirectToAction("Index");
        }

        
        private async Task<Post> RecuperarPost(int id)
        {
            return await _db.Posts.Include(m => m.Tags)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        private async Task CrearPost(EditorPost editorPost)
        {
            var post = new Post();
            post.CopiaValores(editorPost, _asignadorTags);
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
        }

        private async Task ActualizarPost(EditorPost editorPost)
        {
            var post = await RecuperarPost(editorPost.Id);
            post.CopiaValores(editorPost, _asignadorTags);
            await _db.SaveChangesAsync();
        }

        private async Task EliminarPost(int id)
        {
            var post = await RecuperarPost(id);
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
