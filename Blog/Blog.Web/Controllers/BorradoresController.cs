﻿using System;
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
using Blog.ViewModels.Post;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class BorradoresController : Controller
    {
        private readonly PostsServicio _postsServicio;

        public BorradoresController() : this(new ContextoBaseDatos())
        {
            
        }

        public BorradoresController(ContextoBaseDatos contexto): 
            this(new PostsServicio(contexto, 
                    new AsignadorTags(new TagRepositorio(contexto)),
                    new AsignadorCategorias(new CategoriaRepositorio(contexto)), 
                    BlogController.TituloBlog))
        {

        }


        public BorradoresController(PostsServicio postsServicio)
        {
            _postsServicio = postsServicio;
        }

        public async Task<ActionResult> Index()
        {
            var criteriosBusqueda = CriteriosBusqueda.Vacio();
            var viewModel = new ListaBorradoresViewModel
            {
                BuscarPor = criteriosBusqueda,
                ListaPosts = await _postsServicio.ObtenerListaBorradores(criteriosBusqueda)
            };


            return View("Lista", viewModel);
        }
        


        public async Task<ActionResult> Detalles(int? id)
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

            await _postsServicio.CrearBorrador(viewModel);
                
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
            Post post = await RecuperarPost(id.Value);
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

            await ActualizarPost(viewModel);

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
                await ActualizarPost(viewModel);
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

       
        private async Task ActualizarPost(EditorBorrador editorBorrador)
        {
            await _postsServicio.ActualizarBorrador(editorBorrador);
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
