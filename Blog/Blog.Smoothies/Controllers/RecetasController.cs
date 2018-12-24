using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Servicios.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Smoothies.Views.Recetas.ViewModels;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class RecetasController : Controller
    {
        private  readonly BuscadorDeRecetas _buscador;
        private readonly EditorDeRecetas _editor;


        public RecetasController() : this(new ContextoBaseDatos())
        {
            
        }

        public RecetasController(ContextoBaseDatos contexto) : this(
                new BuscadorDeRecetas(contexto), 
                new EditorDeRecetas(contexto))
        {
            
        }

        public RecetasController(BuscadorDeRecetas buscador, EditorDeRecetas editor)
        {
            _buscador = buscador;
            _editor = editor;
        }


        public async Task<ActionResult> Index(string buscarPor = null, int pagina = 1)
        {
            var lineas = await _buscador.BuscarPaginaAsync(new CriteriosBusquedaReceta{ BuscarPor = buscarPor }, pagina, 20);

            var listaRecetasViewModel = new ListaGestionRecetasViewModel
            {
                Recetas = lineas
            };

            return View(listaRecetasViewModel);

        }

        public async Task<ActionResult> VistaPrevia(int id)
        {
            var receta = await _buscador.BuscarRecetaPorId(id);

            if (receta == null) return HttpNotFound();

            var detallesRecetaViewModel = new VistaPreviaRecetaViewModel(receta);
            
            return View(detallesRecetaViewModel);
        }

        public ActionResult Crear()
        {
            var crearRecetaViewModel = new CrearRecetaViewModel
            {
                EditorReceta = new EditorRecetaPartialModel { Autor = "Laura García" }
            };
            return View(crearRecetaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearRecetaViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var editor = viewModel.EditorReceta;

            var comando = new ComandoCrearReceta
            {
                Autor = editor.Autor,
                Nombre = editor.Nombre,
                CategoriaReceta = editor.CategoriaReceta,
                Descripcion = editor.Descripcion,
                FechaPublicacion = editor.FechaPublicacion,
                Keywords = editor.Keywords,
                Raciones = editor.Raciones,
                TiempoCoccion = editor.TiempoCoccion,
                TiempoPreparacion = editor.TiempoPreparacion
            };

           await  _editor.CrearReceta(comando);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Editar(int id)
        {
            var receta = await _buscador.BuscarRecetaPorId(id);

            if (receta == null) return HttpNotFound();

            return View(new EditarRecetaViewModel(receta));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarRecetaViewModel editarRecetaViewModel)
        {
            if (!ModelState.IsValid) return View(editarRecetaViewModel);

            await EditarReceta(editarRecetaViewModel.EditorReceta);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(EditarRecetaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await EditarReceta(viewModel.EditorReceta);
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

        private async Task EditarReceta(EditorRecetaPartialModel editor)
        {
            var comando = new ComandoEditarReceta
            {
                Id = editor.Id,
                Autor = editor.Autor,
                Nombre = editor.Nombre,
                CategoriaReceta = editor.CategoriaReceta,
                Descripcion = editor.Descripcion,
                FechaPublicacion = editor.FechaPublicacion,
                Keywords = editor.Keywords,
                Raciones = editor.Raciones,
                TiempoCoccion = editor.TiempoCoccion,
                TiempoPreparacion = editor.TiempoPreparacion
            };
            await _editor.EditarReceta(comando);
        }


        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var receta =  await _buscador.BuscarRecetaPorId(id.Value);

            if (receta == null) return HttpNotFound();

            var eliminarRecetaViewModel = new EliminarRecetaViewModel(receta);

            return View(eliminarRecetaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(EliminarRecetaViewModel eliminarRecetaViewModel)
        {
            await _editor.EliminarReceta(new ComandoEliminarReceta{ IdReceta =  eliminarRecetaViewModel.Id });

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async  Task<ActionResult> QuickMultipleSearch(string term)
        {
            var search = (await _buscador.ElementosDeRecetas(term))
                            .Select(m => new { value = m.Descripcion, id = m.Valor })
                            .ToList();
            ;
            return Json(search, JsonRequestBehavior.AllowGet);
        }


        
    }
}
