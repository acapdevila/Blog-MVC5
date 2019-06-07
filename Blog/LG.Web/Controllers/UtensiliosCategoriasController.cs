using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Servicios.Utensilios;
using Blog.Servicios.Utensilios.Comandos;
using Blog.Smoothies.Views.UtensiliosCategorias.ViewModels;

namespace LG.Web.Controllers
{
    [Authorize]
    public class UtensiliosCategoriasController : Controller
    {
        private  readonly BuscadorDeCategoriasDeUtensilios _buscador;
        private readonly BuscadorDeCategoriaDeUtensilio _buscadorDeCategoria;
        private readonly EditorDeCategoriasDeUtensilios _editor;
        private readonly GestorPosicionesCategoriasUtensilios _posiciones;

        public UtensiliosCategoriasController() : this(new ContextoBaseDatos())
        {
            
        }

        public UtensiliosCategoriasController(ContextoBaseDatos contexto) : this(
                new BuscadorDeCategoriasDeUtensilios(contexto), 
                new BuscadorDeCategoriaDeUtensilio(contexto), 
                new EditorDeCategoriasDeUtensilios(contexto),
                new GestorPosicionesCategoriasUtensilios(contexto))
        {
            
        }

        public UtensiliosCategoriasController(
            BuscadorDeCategoriasDeUtensilios buscador,
            BuscadorDeCategoriaDeUtensilio buscadorDeCategoria, 
            EditorDeCategoriasDeUtensilios editor,
            GestorPosicionesCategoriasUtensilios posiciones)
        {
            _buscador = buscador;
            _editor = editor;
            _buscadorDeCategoria = buscadorDeCategoria;
            _posiciones = posiciones;
        }


        public async Task<ActionResult> Index(string buscarPor = null, int pagina = 1)
        {
            var listaViewModel = new ListaGestionCategoriasDeUtensiliosViewModel
            {
                Categorias = await _buscador.BuscarPaginaAsync(CriteriosBusqueda.Crear(buscarPor), pagina, 20)
            };

            return View(listaViewModel);
        }


        public ActionResult Crear()
        {
            var posicion = _posiciones.ObtenerPosicionParaNuevaCategoria();
            var crearViewModel = new CrearCategoriaUtensilioViewModel(posicion);
            return View(crearViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearCategoriaUtensilioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Crear", viewModel);

            var comando = viewModel.EditorCategoria.GenerarComandoCrearCategoriaUtensilio();
            
            await  _editor.CrearCategoria(comando);
            
            return RedirectToAction("Index");
        }

    

        public async Task<ActionResult> Editar(int id)
        {
            var categoria = await _buscadorDeCategoria.BuscarCategoriaPorIdAsync(id);

            if (categoria == null) return HttpNotFound();

            return View(new EditarCategoriaUtensilioViewModel(categoria));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarCategoriaUtensilioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Editar", viewModel);
            
            var comando = viewModel.EditorCategoria.GenerarComandoEditarCategoriaUtensilio();

            await _editor.EditarCategoria(comando);

            return RedirectToAction("Index");
        }
        
        

        public async Task<ActionResult> Eliminar(int id)
        {
            var categoria =  await _buscadorDeCategoria.BuscarCategoriaPorIdAsync(id);

            if (categoria == null) return HttpNotFound();

            var eliminarViewModel = new EliminarCategoriaUtensilioViewModel(categoria);

            return View(eliminarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(EliminarCategoriaUtensilioViewModel eliminarViewModel)
        {
            await _editor.EliminarCategoria(new ComandoEliminarCategoriaUtensilio
            {
                IdDeCategoriaDeUtensilio=  eliminarViewModel.Id
            });

            return RedirectToAction("Index");
        }

  

        [AllowAnonymous]
        public async  Task<ActionResult> AutocompleteSearch(string term)
        {
            var search = (await _buscador.ElementosDeCategorias(term))
                            .Select(m => new { value = m.Descripcion, id = m.Valor })
                            .ToList();
            ;
            return Json(search, JsonRequestBehavior.AllowGet);
        }




    }
}
