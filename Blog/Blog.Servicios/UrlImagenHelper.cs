using Blog.Servicios.Configuracion;

namespace Blog.Servicios
{
    public static class UrlImagenHelper
    {
        public static string DirectorioImagenes = "/imagenes";

        public static string GenerarUrlImagen(this string relativefilepath)
        {
            return $"{WebConfigParametro.UrlRaizImagenes}{DirectorioImagenes}{relativefilepath}";
        }

        public static string GenerarUrlImagen(this string relativefilepath, string folderPath)
        {
            return $"{WebConfigParametro.UrlRaizImagenes}{folderPath}{relativefilepath}";
        }
    }
}