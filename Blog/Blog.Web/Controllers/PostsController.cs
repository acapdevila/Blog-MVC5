﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Modelo;
using Blog.Web.ViewModels.Post;
using Blog.Web.ViewModels.Post.Conversores;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ContextoBaseDatos _db = new ContextoBaseDatos();

        public async Task<ActionResult> Index()
        {
            return View(await _db.Posts.ToListAsync());
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
        
        public ActionResult Create()
        {
            var post = Post.CrearNuevoPorDefecto();

            var viewModel = new EditViewModel
            {
                EditorPost = new EditorPost()
            };

            viewModel.EditorPost.CopiaValores(post);

            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await CrearPost(viewModel.EditorPost);
              
                return RedirectToAction("Index");
            }

            return View(viewModel);
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

            var viewModel = new EditViewModel
            {
                EditorPost = new EditorPost()
            };

            viewModel.EditorPost.CopiaValores(post);
            
            return View(viewModel);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await ActualizarPost(viewModel.EditorPost);
              
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
            return await _db.Posts.Include(m => m.Tags)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        private async Task CrearPost(EditorPost editorPost)
        {
            var post = new Post();
            post.CopiaValores(editorPost);
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
        }

        private async Task ActualizarPost(EditorPost editorPost)
        {
            var post = await RecuperarPost(editorPost.Id);
            post.CopiaValores(editorPost);

            await _db.SaveChangesAsync();
        }

        private async Task EliminarPost(int id)
        {
            var post = await RecuperarPost(id);
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
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
