using System.Configuration;

namespace Blog.Web.Configuracion
{
    public static class WebConfigParametro 
    {
        public static string EmailContactoBlog => ConfigurationManager.AppSettings["EmailContactoBlog"];
        public static string EmailBlog => ConfigurationManager.AppSettings["EmailEnvioCorreos"];
    }

}
