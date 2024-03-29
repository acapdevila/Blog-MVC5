﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Ac.Datos;
using Ac.Dominio;
using Ac.Dominio.Posts;
using Ac.Web.ViewModels.Blog;

namespace Ac.Web.Controllers
{
    public class PortadaController : Controller
    {
        public class PortadaViewModel
        {
            public List<LineaResumenPost> UltimosPosts { get; set; }
         //   public PostViewModel UltimoPostDeLecturasRecomendadas { get; set; }
        }

        private readonly ContextoBaseDatos _db;
        public PortadaController()
        {
            _db = new ContextoBaseDatos();
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "none", NoStore = true)]
        public async Task<ActionResult> Index()   
        {
            var viewModel = new PortadaViewModel
            {
                UltimosPosts = await RecuperarPostPortada(),
                //UltimoPostDeLecturasRecomendadas = await RecuperarUltimoPostDeLecturasRecomendadas()
            };
            return View(viewModel);
        }

        private async Task<PostViewModel> RecuperarUltimoPostDeLecturasRecomendadas()
        {
            return await Posts()
                .Publicados()
                .DeLecturasRecomendadas()
                .OrderByDescending(m => m.FechaPost)
                .Select(m=> new PostViewModel
                {
                    Titulo = m.Titulo,
                    SubtituloHtml = m.Subtitulo,
                    ContenidoHtml = m.ContenidoHtml
                })
                .FirstAsync();

        }

        private async Task<List<LineaResumenPost>> RecuperarPostPortada()
        {
            return await Posts()
                .Publicados()
                .QueNoSonLecturasRecomendadas()
                .SeleccionaLineaResumenPost()
                .OrderByDescending(m => m.FechaPost)
                .Take(6)
                .ToListAsync();
        }

        public IQueryable<Post> Posts()
        {
            return _db.Posts;
        }
    }
}