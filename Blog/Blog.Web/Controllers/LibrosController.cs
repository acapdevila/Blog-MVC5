using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ViewModels.Libros;

namespace Blog.Web.Controllers
{
    public class LibrosController : Controller
    {
        // GET: Libros
        public ActionResult Index()
        {
            var viewMmodel = new LibrosProgramacionViewmodel
            {

                //public List<LibroViewmodel> LibrosCSharp { get; set; }

                //public List<LibroViewmodel> LibrosArquitectura { get; set; }

                //public List<LibroViewmodel> LibrosDesarroladores { get; set; }

                //public List<LibroViewmodel> LibrosAptitudes { get; set; }

                Categorias = new List<CategoriaLibroViewmodel>
                {

                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Libros de C#",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Empiece a Programar. Un enfoque multiparadigma con C#",
                                LinkHref = "https://amzn.to/2AOv4MP",
                                ImagenSrc = @"\Content\imagenes\libros\EmpieceAProgramarUnEnfoqueMultiparadigmaConCSharp.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "C# 7.0 in a Nutshell",
                                LinkHref = "https://amzn.to/2SNGmIb",
                                ImagenSrc = @"\Content\imagenes\libros\CSharp7InANutshell.jpg"
                            }

                        }
                    }
                }
            };


            return View(viewMmodel);
        }
    }

    
}