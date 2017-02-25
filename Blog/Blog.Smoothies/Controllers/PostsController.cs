using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Datos.Repositorios;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;
using Blog.Servicios;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly PostsServicio _postsServicio;

        public PostsController() : this(new ContextoBaseDatos())
        {
            
        }

        public PostsController(ContextoBaseDatos contexto): this(new PostsServicio(contexto, new AsignadorTags(new TagRepositorio(contexto)), BlogController.TituloBlog))
        {

        }


        public PostsController(PostsServicio postsServicio)
        {
            _postsServicio = postsServicio;
        }

        public async Task<ActionResult> Index(int pagina = 1)
        {
            var viewModel = await ObtenerListaPostViewModel(pagina, postsPorPagina: 50);
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
                EditorPost = _postsServicio.ObtenerNuevoEditorPorDefecto("Laura García")
            };

            viewModel.EditorPost.PostContenidoHtml = @"
                                <p>
                                <span itemprop='totalTime' class='small color1'>10 minutos</span> · 
                                <span itemprop='recipeYield' class='small color1'>1 persona</span>
                                </p>
                                 <strong>Base:</strong>
                                    <ul>
                                    <li itemprop='ingredients'></li>
                                    <li itemprop='ingredients'></li>
                                    <li itemprop='ingredients'></li>
                                    <li itemprop='ingredients'></li>
                                    </ul>

                                    <strong>Arriba:</strong>
                                    <ul>
                                    <li itemprop='ingredients'></li>
                                    <li itemprop='ingredients'></li>
                                    <liitemprop='ingredients'></li>
                                    </ul>";

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
              
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(EditPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarPost(viewModel.EditorPost);
            }
            return Content(string.Empty);
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

        private async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(int numeroPagina, int postsPorPagina)
        {
            return await _postsServicio.ObtenerListaPostViewModel(numeroPagina, postsPorPagina);
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
