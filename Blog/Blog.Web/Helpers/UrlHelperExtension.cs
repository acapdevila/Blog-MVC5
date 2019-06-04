using System;
using System.Web.Mvc;
using Blog.Web;
using Blog.Web.Controllers;

namespace Ac.Web.Helpers
{
    public static class UrlHelperExtension
    {
        public static string RutaUrlBlogPost(this UrlHelper url, DateTime fechaPost, string urlSlug)
        {
            return RutaAmigable(url, urlSlug);
            //var result = url.RouteUrl(RouteConfig.NombreRutaBlogPost,
            //    new
            //    {
            //        dia = fechaPost.Day,
            //        mes = fechaPost.Month,
            //        anyo = fechaPost.Year,
            //        urlSlug
            //    }, url.RequestContext.HttpContext.Request.Url.Scheme);

            //return result;
        }

        public static string RutaAmigable(this UrlHelper url, string urlSlug)
        {
            return url.RouteUrl(RouteConfig.NombreRutaAmigable,
                new
                {
                    urlSlug
                }, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string RutaEtiqueta(this UrlHelper url, string urlEtiqueta)
        {
            return url.RouteUrl(RouteConfig.NombreRutaEtiquetaAmigable,
                new
                {
                    urlEtiqueta
                }, url.RequestContext.HttpContext.Request.Url.Scheme);
        }


        public static string RutaUrlArchivo(this UrlHelper url, int anyo, int mes)
        {
            var result = url.RouteUrl(RouteConfig.NombreRutaArchivoPosts,
                new
                {
                    mes = mes,
                    anyo = anyo
                }, url.RequestContext.HttpContext.Request.Url.Scheme);

            return result;
        }

        public static string RutaUrlRssFeed(this UrlHelper url)
        {
            return url.Action("Feed", "Rss",routeValues: null, 
                protocol: url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string RutaUrlBase(this UrlHelper url)
        {
            return url.Action("Index", "Blog",
                null, url.RequestContext.HttpContext.Request.Url.Scheme);
        }
    }
}
