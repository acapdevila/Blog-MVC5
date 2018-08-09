using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Datos.Repositorios;
using Blog.Modelo.Categorias;
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
        private readonly TagsServicio _tagsServicio;

        private const int NumeroItemsPorPagina = 50;

        public PostsController() : this(new ContextoBaseDatos())
        {
            
        }

        public PostsController(ContextoBaseDatos contexto): this(
            new PostsServicio(contexto, new AsignadorTags(new TagRepositorio(contexto)), new AsignadorCategorias(new CategoriaRepositorio(contexto)), BlogController.TituloBlog), 
            new TagsServicio(contexto, BlogController.TituloBlog))
        {

        }


        public PostsController(PostsServicio postsServicio, TagsServicio tagsServicio)
        {
            _postsServicio = postsServicio;
            _tagsServicio = tagsServicio;
            
        }

        public async Task<ActionResult> Index(string buscarPor, int pagina = 1)
        {
            var criteriosBusqueda = CriteriosBusqueda.Crear(buscarPor).Value;

            List<Tag> tags = _tagsServicio.BuscarTags(criteriosBusqueda);

            criteriosBusqueda.AñadirTags(tags);

            var viewModel = await ObtenerListaPostViewModel(criteriosBusqueda, pagina, NumeroItemsPorPagina);
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

            viewModel.EditorPost.ActualizaBorrador(post);
            
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
            }
            return Content(string.Empty);
        }


        public async Task<ActionResult> Publicar(int id)
        {
            Post post = await RecuperarPost(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PublicarPost(post);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Publicar(string boton, PublicarPost viewModel)
        {
            if (ModelState.IsValid)
            {
                await _postsServicio.PublicarPost(viewModel);

                if (boton.ToLower().Contains("ver"))
                    return RedirectToAction("Details", new { id = viewModel.Id });

                return RedirectToAction("Index", "Blog");
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

        private async Task<ListaGestionPostsViewModel> ObtenerListaPostViewModel(CriteriosBusqueda criteriosBusqueda, int numeroPagina, int postsPorPagina)
        {
            return await _postsServicio.ObtenerListaPostViewModel(criteriosBusqueda, numeroPagina, postsPorPagina);
        }


        private async Task<Post> RecuperarPost(int id)
        {
            return await _postsServicio.RecuperarPost(id);
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
