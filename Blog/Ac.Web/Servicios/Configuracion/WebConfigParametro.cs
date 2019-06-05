using System.Configuration;

namespace Ac.Servicios.Configuracion
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



        // Parametros legales
        public static string AvisoLegalPaginaWeb => ConfigurationManager.AppSettings["AvisoLegal_PaginaWeb"];
        public static string AvisoLegalEmail => ConfigurationManager.AppSettings["AvisoLegal_Email"];
        public static string AvisoLegalNombreLegal => ConfigurationManager.AppSettings["AvisoLegal_NombreLegal"];
        public static string AvisoLegalCifNif => ConfigurationManager.AppSettings["AvisoLegal_CifNif"];
        public static string AvisoLegalDireccion => ConfigurationManager.AppSettings["AvisoLegal_Direccion"];

        }

}
