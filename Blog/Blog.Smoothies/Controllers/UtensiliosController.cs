﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Datos;
using Blog.Smoothies.Views.Utensilios.ViewModels;

namespace Blog.Smoothies.Controllers
{
    public class UtensiliosController : Controller
    {

        private readonly ContextoBaseDatos _db;

        public UtensiliosController() : this(new ContextoBaseDatos())
        {
            
        }

        public UtensiliosController(ContextoBaseDatos db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = new UtensiliosViewModel(
                await _db.CategoriasDeUtensilios
                    .Include(m=>m.Utensilios)
                    .ToListAsync());

            return View(viewModel);
        }
    }
}