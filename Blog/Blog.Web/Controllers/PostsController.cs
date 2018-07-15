using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Datos.Repositorios;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly PostsServicio _postsServicio;

        public PostsController() : this(new ContextoBaseDatos())
        {
            
        }

        public PostsController(ContextoBaseDatos contexto): 
            this(new PostsServicio(contexto, 
                    new AsignadorTags(new TagRepositorio(contexto)),
                    new AsignadorCategorias(new CategoriaRepositorio(contexto)), 
                    BlogController.TituloBlog))
        {

        }


        public PostsController(PostsServicio postsServicio)
        {
            _postsServicio = postsServicio;
        }

        public async Task<ActionResult> Index(int pagina = 1)
        {
            var viewModel = await ObtenerListaPostViewModel(pagina, postsPorPagina: 100);
            return View(viewModel);
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
            var viewModel = new EditPostViewModel
            {
                EditorPost = _postsServicio.ObtenerNuevoEditorPorDefecto("Albert Capdevila")
            };
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string boton, EditPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await CrearPost(viewModel.EditorPost);
              
                if(boton.ToLower().Contains(@"salir"))
                return RedirectToAction("Index");

                if (boton.ToLower().Contains(@"ver"))
                    return RedirectToAction("Details", new { id = viewModel.EditorPost.Id });

                return RedirectToAction("Edit", new { viewModel.EditorPost.Id });
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

            var viewModel = new EditPostViewModel
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
        public async Task<ActionResult> Edit(EditPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarPost(viewModel.EditorPost);

                return RedirectToAction("Details", new { id = viewModel.EditorPost.Id });
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(EditPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarPost(viewModel.EditorPost);
                return Json(new { esOk = true }, JsonRequestBehavior.AllowGet);
            }

            var sb = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    sb.AppendLine(error.ErrorMessage);
                }
            }

            return Json(new { esOk = false, textoRespuesta = sb.ToString() }, JsonRequestBehavior.AllowGet);

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

        private async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(int pagina, int postsPorPagina)
        {
            return await _postsServicio.ObtenerListaPostViewModel(CriteriosBusqueda.Vacio(),  pagina, postsPorPagina);
        }


        private async Task<Post> RecuperarPost(int id)
        {
            return await _postsServicio.RecuperarPost(id);
        }

        private async Task CrearPost(EditorPost editorPost)
        {
            await _postsServicio.CrearPost(editorPost);
        }

        private async Task ActualizarPost(EditorPost editorPost)
        {
            await _postsServicio.ActualizarPost(editorPost);
        }

        private async Task EliminarPost(int id)
        {
            await _postsServicio.EliminarPost(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _postsServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
