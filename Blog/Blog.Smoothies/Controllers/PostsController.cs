using System.Linq;
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
using Blog.Servicios.Blog;
using Blog.Servicios.Cache;
using Blog.Servicios.Recetas;
using Blog.ViewModels.Post;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly PostsServicio _postsServicio;
        private readonly BuscadorDeReceta _buscadorDeReceta;
        private readonly BuscadorPostsRelacionados _buscadorPostsRelacionados;

        public PostsController() : this(new ContextoBaseDatos())
        {

        }

        public PostsController(ContextoBaseDatos contexto) :
            this(new PostsServicio(contexto,
                    new AsignadorTags(new TagRepositorio(contexto)),
                    new AsignadorCategorias(new CategoriaRepositorio(contexto)), 
                    BlogController.TituloBlog),
                new BuscadorDeReceta(contexto),
                new BuscadorPostsRelacionados(contexto))
        {

        }


        public PostsController(PostsServicio postsServicio, BuscadorDeReceta buscadorDeReceta, BuscadorPostsRelacionados buscadorPostsRelacionados)
        {
            _postsServicio = postsServicio;
            _buscadorDeReceta = buscadorDeReceta;
            _buscadorPostsRelacionados = buscadorPostsRelacionados;
        }

        public async Task<ActionResult> Index(string buscarPor, int pagina = 1)
        {
            var criteriosBusqueda = CriteriosBusqueda.Crear(buscarPor);

            var viewModel = await _postsServicio.ObtenerListaPostViewModel(criteriosBusqueda, pagina, 100);
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


        public async Task<ActionResult> Editar(int? id)
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
                EditorPost = new EditorPost(post)
            };

            return View(viewModel);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(string boton, EditPostViewModel viewModel)
        {
            if (boton.ToLower().Contains("modificar publicación"))
                return RedirectToAction("Publicar", new { id = viewModel.EditorPost.Id });

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
            string accion = boton.ToLower();

            var post = await _postsServicio.RecuperarPost(viewModel.Id);

            if (accion.Contains("cancelar"))
            {
                if (post.EsBorrador)
                    return RedirectToAction("Editar", "Borradores", new { id = viewModel.Id });

                return RedirectToAction("Editar", "Posts", new { id = viewModel.Id });
            }

            if (ModelState.IsValid)
            {
                var editorPost = new EditorPost(post);
                TryValidateModel(editorPost);
                if (!ModelState.IsValid) return View(viewModel);

                if (accion.Contains("programar"))
                    await _postsServicio.ProgramarPublicacion(viewModel);
                else if (accion.Contains("publicar"))
                    await _postsServicio.PublicarPost(viewModel);

                LimpiarCache();

                if (accion.Contains("programar"))
                    return RedirectToAction("Index", "Borradores");

                if (accion.Contains("home"))
                    return RedirectToAction("Index", "Blog");

                return RedirectToRoute(RouteConfig.NombreRutaAmigable, new { urlSlug = viewModel.UrlSlug });
            }
            return View(viewModel);
        }

        private void LimpiarCache()
        {
            var cache = new CacheService();
            cache.Clear();
        }

        public async Task<ActionResult> Eliminar(int? id)
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

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await EliminarPost(id);
            return RedirectToAction("Index");
        }




        private async Task<Post> RecuperarPost(int id)
        {
            return await _postsServicio.RecuperarPost(id);
        }


        private async Task ActualizarPost(EditorPost editorPost)
        {
            var receta = await _buscadorDeReceta.BuscarRecetaPorNombreAsync(editorPost.Receta);

            var postsRelacionados =
                await _buscadorPostsRelacionados.BuscarPostsRelacionadosPorTitulosAsync(
                    editorPost.PostsRelacionados.Where(m=>!m.EstaMarcadoParaEliminar)
                    .Select(m => m.Nombre).ToList());

            await _postsServicio.ActualizarPost(editorPost, receta, postsRelacionados);
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
