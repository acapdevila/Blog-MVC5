using System.Collections.Generic;
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
                Categorias = new List<CategoriaLibroViewmodel>
                {

                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Libros de C#",
                        Libros = new List<LibroViewmodel>
                        {
                           
                            new LibroViewmodel
                            {
                                Titulo = "C# 7.0 in a Nutshell",
                                LinkHref = "https://amzn.to/2SNGmIb",
                                ImagenSrc = @"\Content\imagenes\libros\CSharp7InANutshell.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "C# in Depth",
                                LinkHref = "https://amzn.to/2Dy5i22",
                                ImagenSrc = @"\Content\imagenes\libros\CSharpInDepth.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Pro C# 7: With .NET and .NET Core",
                                LinkHref = "https://amzn.to/2zHxX06",
                                ImagenSrc = @"\Content\imagenes\libros\ProCsharp7.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Professional C# 7 and .NET Core 2.0",
                                LinkHref = "https://amzn.to/2PkDtAH",
                                ImagenSrc = @"\Content\imagenes\libros\CSharp7AndNetCore.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Introducción a C#: Manual de estudiante",
                                LinkHref = "https://amzn.to/2QDpNNP",
                                ImagenSrc = @"\Content\imagenes\libros\IntroduccionACSharp.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Programación Asíncrona con C#: Manual de estudiante",
                                LinkHref = "https://amzn.to/2DuLbSt",
                                ImagenSrc = @"\Content\imagenes\libros\ProgramacionAsincronaConCsharp.jpg"
                            },
                            new LibroViewmodel
                            {
                            Titulo = "Empiece a Programar. Un enfoque multiparadigma con C#",
                            LinkHref = "https://amzn.to/2AOv4MP",
                            ImagenSrc = @"\Content\imagenes\libros\EmpieceAProgramarUnEnfoqueMultiparadigmaConCSharp.jpg"
                        },
                         
                            
                        },

                    },
                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Código limpio",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Código Limpio: Manual de estilo para el desarrollo ágil de software",
                                LinkHref = "https://amzn.to/2zHyori",
                                ImagenSrc = @"\Content\imagenes\libros\CodigoLimpio.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "The Clean Coder: A Code of Conduct for Professional Programmers",
                                LinkHref = "https://amzn.to/2DuN7ud",
                                ImagenSrc = @"\Content\imagenes\libros\TheCleanCoder.jpg"
                            },
                          
                            new LibroViewmodel
                            {
                                Titulo = "Agile Principles, Patterns, and Practices in C#",
                                LinkHref = "https://amzn.to/2Dwvf1N",
                                ImagenSrc = @"\Content\imagenes\libros\AgilePrinciplesPatternsPracticesCSharp.jpg"
                            },
                           

                            new LibroViewmodel
                            {
                                Titulo = "Working Effectively with Legacy Code",
                                LinkHref = "https://amzn.to/2z60BIK",
                                ImagenSrc = @"\Content\imagenes\libros\WorkingWithLegacyCode.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Arquitectura limpia: Guía para especialistas en la estructura y el diseño de software",
                                LinkHref = "https://amzn.to/2PkDV1R",
                                ImagenSrc = @"\Content\imagenes\libros\ArquitecturaLimpia.jpg"
                            },

                        }
                    },
                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Patrones de diseño",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Patrones de diseño",
                                LinkHref = "https://amzn.to/2QEBfc1",
                                ImagenSrc = @"\Content\imagenes\libros\PatronesDiseno.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Head First Design Patterns (A Brain Friendly Guide)",
                                LinkHref = "https://amzn.to/2RMeYJi",
                                ImagenSrc = @"\Content\imagenes\libros\HeadFirsDessignPatterns.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Refactoring to Patterns",
                                LinkHref = "https://amzn.to/2AYqxaD",
                                ImagenSrc = @"\Content\imagenes\libros\RefactoringToPatterns.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions",
                                LinkHref = "https://amzn.to/2B0rzmD",
                                ImagenSrc = @"\Content\imagenes\libros\EnterpriseIntegrationPatterns.jpg"
                            },
                        }
                    },
                    
                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Domain Driven Design (DDD)",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Domain-Driven Design: Tackling Complexity in the Heart of Software",
                                LinkHref = "https://amzn.to/2AZ2ecF",
                                ImagenSrc = @"\Content\imagenes\libros\DomainDrivenDesign.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Implementing Domain-Driven Design",
                                LinkHref = "https://amzn.to/2PpkQLS",
                                ImagenSrc = @"\Content\imagenes\libros\ImplementingDomainDrivenDesign.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Patterns, Principles, and Practices of Domain-Driven Design",
                                LinkHref = "https://amzn.to/2Pkkh5Y",
                                ImagenSrc = @"\Content\imagenes\libros\PatternsPrinciplesAndPracticesDDD.jpg"
                            },
                        }
                    },
                 
                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Carrera profesional",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Soft Skills: The software developer's life manual",
                                LinkHref = "https://amzn.to/2PiFfCd",
                                ImagenSrc = @"\Content\imagenes\libros\SoftSkills.jpg"
                            },

                            new LibroViewmodel
                            {
                                Titulo = "Joel on Software: And on Diverse and Occasionally Related Matters That Will Prove of Interest to Software Developers, Designers, and Managers, and to...or Ill - Luck, Work with Them in Some Capacity",
                                LinkHref = "https://amzn.to/2ODo3C4",
                                ImagenSrc = @"\Content\imagenes\libros\JoelOnSoftware.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Smart and Gets Things Done: Joel Spolsky's Concise Guide to Finding the Best Technical Talent ",
                                LinkHref = "https://amzn.to/2PmloSt",
                                ImagenSrc = @"\Content\imagenes\libros\GetThingsDone.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Cracking the Coding Interview: 189 Programming Questions and Solutions",
                                LinkHref = "https://amzn.to/2DxDH0Q",
                                ImagenSrc = @"\Content\imagenes\libros\CrackingInterview.jpg"
                            },
                        }
                    } ,

                    new CategoriaLibroViewmodel
                    {
                        Nombre = "Otros",
                        Libros = new List<LibroViewmodel>
                        {
                            new LibroViewmodel
                            {
                                Titulo = "Programming Pearls",
                                LinkHref = "https://amzn.to/2DeiwA3",
                                ImagenSrc = @"\Content\imagenes\libros\ProgrammingPerls.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "Refactoring: Improving the Design of Existing Code",
                                LinkHref = "https://amzn.to/2z6eMxx",
                                ImagenSrc = @"\Content\imagenes\libros\RefactoringImprovingDesign.jpg"
                            },
                            new LibroViewmodel
                            {
                                Titulo = "No me hagas pensar",
                                LinkHref = "https://amzn.to/2Ow74Sl",
                                ImagenSrc = @"\Content\imagenes\libros\NoMeHagasPensar.jpg"
                            },
                        }
                },

                }
            };


            return View(viewMmodel);
        }
    }

    
}