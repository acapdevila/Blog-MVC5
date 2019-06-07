using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Servicios;
using Blog.Servicios.Utensilios;
using Blog.Servicios.Utensilios.Comandos;
using Blog.Smoothies.Helpers;
using Blog.Smoothies.Views.Shared.ViewModels;
using Blog.Smoothies.Views.UtensiliosCategorias.ViewModels;
using Blog.Smoothies.Views.UtensiliosGestion.ViewModels;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class UtensiliosGestionController : Controller
    {
        private readonly BuscadorDeUtensilios _buscador;
        private readonly BuscadorDeUtensilio _buscadorDeUtensilio;
        private readonly EditorDeUtensilios _editor;
        private readonly SubirArchivoImagenServicio _imagenServicio;

        public UtensiliosGestionController() : this(new ContextoBaseDatos())
        {

        }

        public UtensiliosGestionController(ContextoBaseDatos contexto) : this(
                new BuscadorDeUtensilios(contexto),
                new BuscadorDeUtensilio(contexto),
                new EditorDeUtensilios(contexto),
                new SubirArchivoImagenServicio())
        {

        }

        public UtensiliosGestionController(
            BuscadorDeUtensilios buscador,
            BuscadorDeUtensilio buscadorDeCategoria,
            EditorDeUtensilios editor,
            SubirArchivoImagenServicio imagenServicio)
        {
            _buscador = buscador;
            _editor = editor;
            _buscadorDeUtensilio = buscadorDeCategoria;
            _imagenServicio = imagenServicio;
        }


        public async Task<ActionResult> Index(string buscarPor = null, int pagina = 1)
        {
            var listaViewModel = new ListaGestionDeUtensiliosViewModel
            {
                Utensilios = await _buscador.BuscarPaginaAsync(CriteriosBusqueda.Crear(buscarPor), pagina, 20)
            };

            return View(listaViewModel);
        }


        public ActionResult Crear()
        {
            var crearViewModel = new CrearUtensilioViewModel(
                "CrearUtensilio",
                "CrearSubirImagen", 
                "CrearQuitarImagen");
            
            return View(crearViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public ActionResult CrearSubirImagen(CrearUtensilioViewModel viewModel)
        {
            RellenarUrlImagen(viewModel.Editor.EditorImagen);
            return View("Crear", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public ActionResult CrearQuitarImagen(CrearUtensilioViewModel viewModel)
        {
            VaciarUrlImagen(viewModel.Editor.EditorImagen);
            return View("Crear", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public async Task<ActionResult> CrearUtensilio(CrearUtensilioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Crear", viewModel);

            var comando = viewModel.Editor.GenerarComandoCrearUtensilio();

            await _editor.CrearUtensilio(comando);

            return RedirectToAction("Index");
        }



        public async Task<ActionResult> Editar(int id)
        {
            var utensilio = await _buscadorDeUtensilio.BuscarUtensilioPorIdAsync(id);

            if (utensilio == null) return HttpNotFound();

            var editarViewModel = new EditarUtensilioViewModel(
                utensilio,
                "EditarUtensilio",
                "EditarSubirImagen",
                "EditarQuitarImagen"
                );

            return View(editarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public ActionResult EditarSubirImagen(EditarUtensilioViewModel viewModel)
        {
            RellenarUrlImagen(viewModel.Editor.EditorImagen);
            return View("Editar", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public ActionResult EditarQuitarImagen(EditarUtensilioViewModel viewModel)
        {
            VaciarUrlImagen(viewModel.Editor.EditorImagen);
            return View("Editar", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccionSeleccionadaPorBoton]
        public async Task<ActionResult> EditarUtensilio(EditarUtensilioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Editar", viewModel);

            var comando = viewModel.Editor.GenerarComandoEditarUtensilio();

            await _editor.EditarUtensilio(comando);

            return RedirectToAction("Index");
        }



        public async Task<ActionResult> Eliminar(int id)
        {
            var utensilio = await _buscadorDeUtensilio.BuscarUtensilioPorIdAsync(id);

            if (utensilio == null) return HttpNotFound();

            var eliminarViewModel = new EliminarUtensilioViewModel(utensilio);

            return View(eliminarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(EliminarCategoriaUtensilioViewModel eliminarViewModel)
        {
            await _editor.EliminarUtensilio(new ComandoEliminarUtensilio
            {
                IdDeUtensilio = eliminarViewModel.Id
            });

            return RedirectToAction("Index");
        }



        [AllowAnonymous]
        public async Task<ActionResult> AutocompleteSearch(string term)
        {
            var search = (await _buscador.ElementosDeUtensilios(term))
                            .Select(m => new { value = m.Descripcion, id = m.Valor })
                            .ToList();
            ;
            return Json(search, JsonRequestBehavior.AllowGet);
        }


        #region Privados

        private HttpPostedFileBase ObtenerArchivoImagenDelHttpPost()
        {
            return Request.Files.Count == 0 ? null : Request.Files.Get(0);
        }

        private void RellenarUrlImagen(EditorImagen editor)
        {
            var imagenSubida = ObtenerArchivoImagenDelHttpPost();

            var nombreImgenGuardada = _imagenServicio.SubirImagen(imagenSubida.ToWebImage(), 1000);

            ModelState.Clear();

            editor.UrlImagen = nombreImgenGuardada.GenerarUrlImagen();
        }

        private void VaciarUrlImagen(EditorImagen editor)
        {
            ModelState.Clear();
            editor.UrlImagen = null;
        }

        #endregion
    }
}