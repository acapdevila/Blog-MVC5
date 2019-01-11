using System.Web.Mvc;
using System.Web.Routing;
using Blog.Servicios.Cache;
using Blog.Servicios.Rutas;
using Blog.Smoothies.Controllers;

namespace Blog.Smoothies
{
    

    public class RouteConfig
    {
        public const string NombreRutaMvc = "RutaMvc";
        public const string NombreRutaPostConFecha = "RutaPostConFecha";
        public const string NombreRutaAmigable = "RutaAmigable";
        public const string NombreRutaCategoriaAmigable = "RutaCategoriaAmigable";
        public const string NombreRutaArchivoPosts = "RutaArchivoPots";
        public const string NombreRutaSitemap = "RutaSitemapXml";
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
            // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;

            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("humans.txt");


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: NombreRutaSitemap,
                url: "sitemapxml",
                defaults: new { controller = "Sitemap", action = "Xml"}
            );

            routes.MapRoute(
              name: NombreRutaPostConFecha,
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

            var servicioCahce = new CacheService();

            routes.MapRoute(
                name: NombreRutaAmigable,
                url: "{urlSlug}",
                defaults: new { controller = "Blog", action = "DetallesAmigable" },
                constraints: new { urlSlug = new RutaPostConstraint(servicioCahce, BlogController.TituloBlog) });

           routes.MapRoute(
                name: NombreRutaCategoriaAmigable,
                url: "{urlCategoria}",
                defaults: new { controller = "Blog", action = "CategoriaAmigable" },
                constraints: new { urlCategoria = new RutaCategoriaAmigableConstraint(servicioCahce, BlogController.TituloBlog) });
            


            routes.MapRoute(
                name: NombreRutaMvc,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
                constraints: new
                    {
                        controller = "Account|Blog|Blogs|Borradores|Categorias|Contacto|Error|Hola|Imagenes|Libros|Manage|Menu|Portada|Posts|Principal|Recetas|Rss|Sidebar|Tags|Utensilios"
                }
            );



            routes.MapRoute(
                name: "RutaNoEncontrada",
                url: "{*restoderutas}",
                defaults: new { controller = "Error", action = "NotFound" }
            );




        }
    }
}
