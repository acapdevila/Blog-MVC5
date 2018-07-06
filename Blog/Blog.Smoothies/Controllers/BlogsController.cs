using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Servicios;
using Blog.ViewModels.Post;
using Blog.ViewModels.Post.Conversores;

namespace Blog.Smoothies.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly BlogsServicio _blogServicio;

        public BlogsController() : this(new BlogsServicio(new ContextoBaseDatos()))
        {
            
        }
        


        public BlogsController(BlogsServicio blogServicio)
        {
            _blogServicio = blogServicio;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = await ObtenerListaBlogsViewModel();
            return View(viewModel);
        }

  

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = await RecuperarBlog(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        public async Task<ActionResult> EditarPaginaPrincipal()
        {
            var blog = await _blogServicio.RecuperarBlogPorTitulo(BlogController.TituloBlog);
            if (blog == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditBlogViewModel
            {
                EditorBlog = new EditorBlog()
            };

            viewModel.EditorBlog.CopiaValores(blog);

            return View("Edit", viewModel);
        }


        public ActionResult Create()
        {
            var post = BlogEntidad.CrearNuevoPorDefecto();

            var viewModel = new EditBlogViewModel
            {
                EditorBlog = new EditorBlog()
            };

            viewModel.EditorBlog.CopiaValores(post);
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string boton, EditBlogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await CrearBlog(viewModel.EditorBlog);
              
                if(boton.ToLower().Contains(@"salir"))
                return RedirectToAction("Index");
                
                return RedirectToAction("Edit", new { viewModel.EditorBlog.Id });
            }

            return View(viewModel);
        }
        

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = await RecuperarBlog(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditBlogViewModel
            {
                EditorBlog = new EditorBlog()
            };

            viewModel.EditorBlog.CopiaValores(blog);
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBlogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarBlog(viewModel.EditorBlog);
              
                return RedirectToAction("Index", "Blog");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Guardar(EditBlogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarBlog(viewModel.EditorBlog);
            }
            return Content(string.Empty);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = await RecuperarBlog(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await EliminarBlog(id);
            return RedirectToAction("Index");
        }

        private async Task<ListaGestionBlogsViewModel> ObtenerListaBlogsViewModel()
        {
            return await _blogServicio.ObtenerListaBlogsViewModel();
        }

        private async Task<BlogEntidad> RecuperarBlog(int id)
        {
            return await _blogServicio.RecuperarBlog(id);
        }

        private async Task CrearBlog(EditorBlog editorBlog)
        {
            await _blogServicio.CrearBlog(editorBlog);
        }

        private async Task ActualizarBlog(EditorBlog editorBlog)
        {
            await _blogServicio.ActualizarBlog(editorBlog);
        }

        private async Task EliminarBlog(int id)
        {
            await _blogServicio.EliminarBlog(id);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _blogServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
