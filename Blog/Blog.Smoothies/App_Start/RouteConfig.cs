using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Smoothies
{
    public class RouteConfig
    {
        public const string NombreRutaPorDefecto = "Default";
        public const string NombreRutaBlogPost = "BlogPost";
        public const string NombreRutaArchivoPosts = "ArchivoPots";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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
                name: NombreRutaPorDefecto,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
