using System;
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
using Blog.Servicios.Blog;
using Blog.Servicios.Blog.Borradores;
using Blog.Servicios.Recetas;
using Blog.ViewModels.Post;
using EditorBorrador = Blog.ViewModels.Post.EditorBorrador;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class BorradoresController : Controller
    {
        private readonly BuscadorBorrador _buscadorBorrador;
        private readonly BuscadorBorradores _buscadorBorradores;
        private readonly BuscadorDeReceta _buscadorDeReceta;
        private readonly EditorBorradorPost _editorBorrador;

        public BorradoresController() : this(new ContextoBaseDatos())
        {
            
        }

        public BorradoresController(ContextoBaseDatos contexto) :
            this(contexto,
                new BuscadorBorrador(contexto, BlogController.TituloBlog),
                new AsignadorTags(new TagRepositorio(contexto)),
                new AsignadorCategorias(new CategoriaRepositorio(contexto)))

        {

        }


        public BorradoresController(
            ContextoBaseDatos contexto,
            BuscadorBorrador buscadorBorrador,
            AsignadorTags asignadorTags,
            AsignadorCategorias asignadorCategorias) :
            this(buscadorBorrador,
                new BuscadorBorradores(contexto, BlogController.TituloBlog), 
                new EditorBorradorPost(contexto, 
                                        new BuscadorBlog(contexto, BlogController.TituloBlog), 
                                        buscadorBorrador, 
                                        asignadorTags, 
                                        asignadorCategorias),
                new BuscadorDeReceta(contexto))
        {

        }


        public BorradoresController(
            BuscadorBorrador buscadorBorrador,
            BuscadorBorradores buscadorBorradores,
            EditorBorradorPost creadorBorrador, 
            BuscadorDeReceta buscadorDeReceta )
        {
            _buscadorDeReceta = buscadorDeReceta;
            _editorBorrador = creadorBorrador;
            _buscadorBorradores = buscadorBorradores;
            _buscadorBorrador = buscadorBorrador;
        }

        public async Task<ActionResult> Index()
        {
            var criteriosBusqueda = CriteriosBusqueda.Vacio();
            var viewModel = new ListaBorradoresViewModel
            {
                BuscarPor = criteriosBusqueda,
                ListaPosts = await _buscadorBorradores.ObtenerListaBorradoresAsync(criteriosBusqueda)
            };


            return View("Lista", viewModel);
        }

        public async Task<ActionResult> ActualizarNombresSinAcentos()
        {
           await  _editorBorrador.ActualizarNombresSinAcentos();
            return RedirectToAction("Index");
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
            if (!(TempData["TempEditorBorrador"] is EditorBorrador viewModel))
            {
                viewModel = new EditorBorrador
                {
                    Autor = "Laura García",
                    FechaPost = DateTime.Today
                };
            }

            return View(viewModel);
        }

        public async Task<ActionResult> CrearDeReceta(int id)
        {
            var receta = await _buscadorDeReceta.BuscarRecetaPorIdAsync(id);
            var post = _editorBorrador.GenerarNuevoBorradorPorReceta(receta);

            TempData["TempEditorBorrador"] = new EditorBorrador(post);

            return RedirectToAction("Crear");
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

            var receta =  await _buscadorDeReceta.BuscarRecetaPorNombreAsync(viewModel.Receta);
            await _editorBorrador.CrearBorrador(viewModel, receta);

            if (boton.ToLower().Contains(@"salir"))
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
            return await _buscadorBorrador.BuscarBorradorPorIdAsync(id);
        }

       
        private async Task ActualizarBorrador(EditorBorrador editorBorrador)
        {
            var receta = await _buscadorDeReceta.BuscarRecetaPorNombreAsync(editorBorrador.Receta);
            await _editorBorrador.ActualizarBorrador(editorBorrador, receta);
        }

        private async Task EliminarBorrador(int id)
        {
            var borrador = await RecuperarBorrador(id);
            await _editorBorrador.EliminarBorrador(borrador);
        }

    }
}
