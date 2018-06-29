using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Datos;
using Blog.Smoothies.Controllers;
using Blog.Smoothies.RutasAmigables;

namespace Blog.Smoothies
{
    

    public class RouteConfig
    {
        public const string NombreRutaPorDefecto = "Default";
        public const string NombreRutaBlogPost = "BlogPost";
        public const string NombreRutaAmigable = "Amigable";
        public const string NombreRutaCategoriaAmigable = "CategoriaAmigable";
        public const string NombreRutaArchivoPosts = "ArchivoPots";
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


           
            var controllerConstraint = 


            routes.MapRoute(
              name: NombreRutaBlogPost,
              url: "{dia}/{mes}/{anyo}/{urlSlug}",
              defaults: new { controller = "Blog", action = "Detalles" },
                 constraints: new
                 {
                     dia = @"\d{1,2}",
                     mes = @"\d{1,2}",
                     anyo = @"\d{4}"
                 }
          );

            routes.MapRoute(
                      name: NombreRutaArchivoPosts,
                      url: "Blog/Archivo/{anyo}/{mes}",
                      defaults: new { controller = "Blog", action = "Archivo" },
                      constraints: new
                      {
                          mes = @"\d{1,2}",
                          anyo = @"\d{4}"
                      }
            );


            routes.MapRoute(
                name: NombreRutaAmigable,
                url: "{urlSlug}",
                defaults: new { controller = "Blog", action = "DetallesAmigable" },
                constraints: new { urlSlug = new RutaAmigableConstraint() });

           routes.MapRoute(
                name: NombreRutaCategoriaAmigable,
                url: "{urlCategoria}",
                defaults: new { controller = "Blog", action = "CategoriaAmigable" },
                constraints: new { urlCategoria = new RutaCategoriaAmigableConstraint() });
            


            routes.MapRoute(
                name: NombreRutaPorDefecto,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
                constraints: new
                    {
                        controller = "Account|Blog|Blogs|Categorias|Contacto|Error|Hola|Imagenes|Manage|Menu|Posts|Principal|Rss|Sidebar|Tags"
                    }
            );



            routes.MapRoute(
                name: "RutaNoEncontrada",
                url: "{*restoderutas}",
                defaults: new { controller = "Error", action = "NotFound"}
            );




        }
    }
}
