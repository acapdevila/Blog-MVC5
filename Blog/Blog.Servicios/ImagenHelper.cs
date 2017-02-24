using System;
using System.Web;
using System.Web.Helpers;
using Blog.Servicios.Configuracion;

namespace Blog.Servicios
{
    public static class ImagenHelper
    {
        public static WebImage ToWebImage(this HttpRequestBase request)
        {
            if (request.Files.Count == 0) return null;

            var postedFile = request.Files[0];
            var image = new WebImage(postedFile.InputStream)
            {
                FileName = postedFile.FileName
            };
            return image;
        }

        public static bool TerminaConUnaExtensionDeImagenValida(this string nombreArchivo)
        {
            return nombreArchivo.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                nombreArchivo.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
                nombreArchivo.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
                nombreArchivo.EndsWith(".JPG", StringComparison.CurrentCultureIgnoreCase) ||
                nombreArchivo.EndsWith(".GIF", StringComparison.CurrentCultureIgnoreCase) ||
                nombreArchivo.EndsWith(".PNG", StringComparison.CurrentCultureIgnoreCase);
        }

        public static WebImage ToWebImage(this HttpPostedFileBase postedFile)
        {
            if (postedFile == null) return null;
            
            var image = new WebImage(postedFile.InputStream)
            {
                FileName = postedFile.FileName
            };
            return image;
        }
        


        public static string DirectorioImagenes = "/imagenes";

        public static string GenerarUrlImagen(this string relativefilepath)
        {
            return string.Format("{0}{1}{2}", 
                WebConfigParametro.UrlRaizImagenes,
                DirectorioImagenes, 
                relativefilepath);
        }

        public static string GenerarUrlImagen(this string relativefilepath, string folderPath)
        {
            return $"{WebConfigParametro.UrlRaizImagenes}{folderPath}{relativefilepath}";
        }
    }
}