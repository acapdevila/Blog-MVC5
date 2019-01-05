using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Servicios;
using Blog.Servicios.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Smoothies.Views.Recetas.ViewModels;
using Blog.Smoothies.Views.Recetas.ViewModels.Editores;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class RecetasController : Controller
    {
        private  readonly BuscadorDeRecetas _buscador;
        private readonly EditorDeRecetas _editor;
        private readonly SubirArchivoImagenServicio _imagenServicio;

        public RecetasController() : this(new ContextoBaseDatos())
        {
            
        }

        public RecetasController(ContextoBaseDatos contexto) : this(
                new BuscadorDeRecetas(contexto), 
                new EditorDeRecetas(contexto),
                new SubirArchivoImagenServicio())
        {
            
        }

        public RecetasController(BuscadorDeRecetas buscador, EditorDeRecetas editor, SubirArchivoImagenServicio imagenServicio)
        {
            _buscador = buscador;
            _editor = editor;
            _imagenServicio = imagenServicio;
        }


        public async Task<ActionResult> Index(string buscarPor = null, int pagina = 1)
        {
            var listaRecetasViewModel = new ListaGestionRecetasViewModel
            {
                Recetas = await _buscador.BuscarPaginaAsync(new CriteriosBusquedaReceta { BuscarPor = buscarPor }, pagina, 20)
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
                EditorReceta = new EditorReceta { Autor = "Laura García" }
            };
            return View(crearRecetaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(HttpPostedFileBase archivoImagen, CrearRecetaViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var urlImagenPublica = PublicarImagen(archivoImagen);

            var comando = viewModel.EditorReceta.GenerarComandoCrearReceta(urlImagenPublica);
            
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
        public async Task<ActionResult> Editar(HttpPostedFileBase archivoImagen, EditarRecetaViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var urlImagen = PublicarImagen(archivoImagen);
            
            var comando = viewModel.EditorReceta.GenerarComandoEditarReceta(urlImagen);

            await _editor.EditarReceta(comando);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(HttpPostedFileBase archivoImagen, EditarRecetaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { esOk = false, textoRespuesta = ErroresDelModelState() }, JsonRequestBehavior.AllowGet);
            }

            var urlImagen = PublicarImagen(archivoImagen);

            var comando = viewModel.EditorReceta.GenerarComandoEditarReceta(urlImagen);
            await _editor.EditarReceta(comando);
            return Json(new { esOk = true }, JsonRequestBehavior.AllowGet);
         }

      


        public async Task<ActionResult> Eliminar(int id)
        {
            var receta =  await _buscador.BuscarRecetaPorId(id);

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


        private string ErroresDelModelState()
        {
            var sb = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    sb.AppendLine(error.ErrorMessage);
                }
            }

            return sb.ToString();
        }


        private string PublicarImagen(HttpPostedFileBase archivoImagen)
        {
            if (archivoImagen == null) return null;
            var webImage = archivoImagen.ToWebImage();
            return _imagenServicio.SubirImagen(webImage, dimensionMaxima: 700);
        }

    }
}
