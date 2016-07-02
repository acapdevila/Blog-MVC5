using System.Configuration;

namespace Blog.Servicios.Configuracion
{
    public static class WebConfigParametro 
    {
        public static string EmailContactoBlog => ConfigurationManager.AppSettings["EmailContactoBlog"];
        public static string EmailBlog => ConfigurationManager.AppSettings["EmailEnvioCorreos"];

        public static string StorageConnectionString => ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
        public static string NombreContenedorBlobAzure => ConfigurationManager.AppSettings["NombreContenedorBlobAzure"];
        public static string UrlRaizImagenes => ConfigurationManager.AppSettings["UrlRaizBlobAzure"];

        public static string DisqusShortname => ConfigurationManager.AppSettings["DisqusShortname"];

        public static string NombreAplicacion => ConfigurationManager.AppSettings["NombreAplicacion"];
    }

}
