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
using Ac.Modelo.Tags;
using Ac.Web.ViewModels.Post;
using Omu.ValueInjecter;

namespace Ac.Web.Controllers
{
    [Authorize]
    public class BorradoresController : Controller
    {
        private readonly ContextoBaseDatos _db;
        private readonly AsignadorTags _asignadorTags;

        public BorradoresController() : this(new ContextoBaseDatos())
        {
            
        }
        public BorradoresController(ContextoBaseDatos contexto) : this(contexto,
            new AsignadorTags(new TagRepositorio(contexto)))
        {

        }


        public BorradoresController(ContextoBaseDatos contexto, AsignadorTags asignadorTags)
        {
            _db = contexto;
            _asignadorTags = asignadorTags;
        }

        
        public async Task<ActionResult> Index()
        {
            var viewModel = new ListaBorradoresViewModel
            {
                ListaPosts = await _db.Posts
                    .Borradores()
                    .Select(m => new LineaBorrador
                    {
                        Id = m.Id,
                        UrlSlug = m.UrlSlug,
                        Titulo = m.Titulo,
                        FechaPost = m.FechaPost,
                        FechaPublicacion = m.EsBorrador ? (DateTime?)null : m.FechaPublicacion,
                        Autor = m.Autor,
                        ListaTags = m.Tags
                    })
                    .OrderByDescending(m => m.FechaPost)
                    .ToListAsync()
             };

            return View("Lista", viewModel);
        }
        


        public async Task<ActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await RecuperarBorrador(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        
        public ActionResult Crear()
        {
            var viewModel = new EditorBorrador
            {
                Autor = "Albert Capdevila",
                FechaPost = DateTime.Today
            };
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(string boton, EditorBorrador viewModel)
        {
            if (boton.ToLower().Contains(@"publicar"))
            {
                var editorPost = new EditorPost(viewModel);

                TryValidateModel(editorPost);

                if (!ModelState.IsValid) return View(viewModel);
            }

            if (!ModelState.IsValid) return View(viewModel);

            await CrearBorrador(viewModel);
                
            if(boton.ToLower().Contains(@"salir"))
              return RedirectToAction("Index");

            if (boton.ToLower().Contains(@"ver"))
               return RedirectToAction("Detalles", new { id = viewModel.Id });


            if (boton.ToLower().Contains(@"publicar"))
                return RedirectToAction("Publicar", "Posts", new { id = viewModel.Id });

            return RedirectToAction("Editar", new { viewModel.Id });
            
        }
        

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await RecuperarBorrador(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditorBorrador(post);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(string boton, EditorBorrador viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await ActualizarBorrador(viewModel);

            if (boton.ToLower().Contains(@"publicar"))
            {
                var editorPost = new EditorPost(viewModel);

                TryValidateModel(editorPost);

                if (!ModelState.IsValid) return View(viewModel);

                return RedirectToAction("Publicar", "Posts", new { id = viewModel.Id });

            }

            return RedirectToAction("Detalles", new { id = viewModel.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(EditorBorrador viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarBorrador(viewModel);
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

    

        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await RecuperarBorrador(id.Value);
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
            await EliminarBorrador(id);
            return RedirectToAction("Index");
        }

        
        private async Task<Post> RecuperarBorrador(int id)
        {
            return await Posts()
                .Borradores()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearBorrador(
            EditorBorrador editorBorrador)
        {
            var post = Post.CrearNuevoPorDefecto(editorBorrador.Autor);
            ActualizaBorrador(post, editorBorrador, _asignadorTags);

            _db.Posts.Add(post);
            await _db.GuardarCambios();
            editorBorrador.Id = post.Id;
        }


        private async Task ActualizarBorrador(EditorBorrador editorBorrador)
        {
            var post = await RecuperarBorrador(editorBorrador.Id);
            ActualizaBorrador(post, editorBorrador, _asignadorTags);
            await _db.GuardarCambios();
        }

        private async Task EliminarBorrador(int id)
        {
            var borrador = await RecuperarBorrador(id);
            _db.Posts.Remove(borrador);
            await _db.GuardarCambios();
        }

        public static void ActualizaBorrador(Post post,
            EditorBorrador editorBorrador,
            AsignadorTags asignadorTags)
        {
            post.InjectFrom(editorBorrador);

            post.ModificarTitulo(editorBorrador.Titulo);

            asignadorTags.AsignarTags(post, editorBorrador.ListaTags);
            post.FechaModificacion = DateTime.Now;
        }


        private IQueryable<Post> Posts()
        {
            return _db.Posts
                .Include(m => m.Tags);
        }

    }
}
