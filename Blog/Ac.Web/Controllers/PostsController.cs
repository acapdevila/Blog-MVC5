using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ac.Datos;
using Ac.Datos.Repositorios;
using Ac.Modelo;
using Ac.Modelo.Posts;
using Ac.Modelo.Tags;
using Ac.Web.ViewModels.Post;
using Infra.Cache;
using PagedList.EntityFramework;

namespace Ac.Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ContextoBaseDatos _db;
        private readonly AsignadorTags _asignadorTags;


        public PostsController() : this(new ContextoBaseDatos())
        {
            
        }

        public PostsController(ContextoBaseDatos contexto) : this(contexto,
            new AsignadorTags(new TagRepositorio(contexto)))
        {

        }
        public PostsController(ContextoBaseDatos contexto, AsignadorTags asignadorTags)
        {
            _db = contexto;
            _asignadorTags = asignadorTags;
        }



        public async Task<ActionResult> Index(int pagina = 1)
        {
               var viewModel = new ListaGestionPostsViewModel
                {
                    BuscarPor = CriteriosBusqueda.Vacio().BuscarPor,
                    ListaPosts = await _db.Posts
                        .Publicados()
                        .Select(m => new LineaGestionPost
                        {
                            Id = m.Id,
                            UrlSlug = m.UrlSlug,
                            Titulo = m.Titulo,
                            FechaPost = m.FechaPost,
                            EsRssAtom = m.EsRssAtom,
                            FechaPublicacion = m.FechaPublicacion,
                            Autor = m.Autor,
                            ListaTags = m.Tags
                        })
                        .OrderByDescending(m => m.FechaPost)
                        .ToPagedListAsync(pagina,100)
                };
            


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
                EditorPost = new EditorPost(post)
            };

            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string boton, EditPostViewModel viewModel)
        {
            if (boton.ToLower().Contains("modificar publicación"))
                return RedirectToAction("Publicar", new {id = viewModel.EditorPost.Id});

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
            var post = await RecuperarPost(viewModel.Id);

            if (accion.Contains("cancelar"))
            {
                if (post.EsBorrador)
                    return RedirectToAction("Editar", "Borradores", new { id = viewModel.Id });

                return RedirectToAction("Edit", "Posts", new { id = viewModel.Id });
            }

            if (ModelState.IsValid)
            {
                var editorPost = new EditorPost(post);
                TryValidateModel(editorPost);
                if (!ModelState.IsValid) return View(viewModel);
                
                if (accion.Contains("programar"))
                    await ProgramarPublicacion(viewModel);
                else if (accion.Contains("publicar"))
                    await PublicarPost(viewModel);

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
            return await Posts()
                .Include(m => m.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        private async Task ActualizarPost(EditorPost editorPost)
        {
            var post = await RecuperarPost(editorPost.Id);
            ActualizaPost(post, editorPost, _asignadorTags);
            
            await _db.GuardarCambios();
            }

        private async Task EliminarPost(int id)
        {
            var post = await RecuperarPost(id);
            _db.Posts.Remove(post);
            await _db.GuardarCambios();
        }

        public static void ActualizaPost(Post post,
            EditorPost editorPost,
            AsignadorTags asignadorTags)
        {
            post.ModificarTitulo(editorPost.Titulo);
            post.Subtitulo = editorPost.Subtitulo;
            post.Descripcion = editorPost.Descripcion;
            post.Autor = editorPost.Autor;
            post.ContenidoHtml = editorPost.ContenidoHtml;
            post.PalabrasClave = editorPost.PalabrasClave;
            post.UrlImagenPrincipal = editorPost.UrlImagenPrincipal;

            asignadorTags.AsignarTags(post, editorPost.ListaTags);
            post.FechaModificacion = DateTime.Now;

        }

        private async Task ProgramarPublicacion(PublicarPost editor)
        {
            var post = await RecuperarPost(editor.Id);
            post.ProgramarPublicacion(editor.FechaPost, editor.UrlSlug, editor.EsRssAtom, editor.FechaPublicacion);
            await _db.GuardarCambios();
        }

        private async Task PublicarPost(PublicarPost editor)
        {
            var post = await RecuperarPost(editor.Id);
            post.Publicar(editor.FechaPost, editor.UrlSlug, editor.EsRssAtom);
            await _db.GuardarCambios();
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts;
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
