using System;
using System.Web.Mvc;

namespace Blog.Smoothies.Helpers
{
    public static class UrlHelperExtension
    {
        public static string RutaUrlBlogPost(this UrlHelper url, DateTime fechaPost, string urlSlug)
        {
            return RutaAmigable(url, urlSlug);
            //return url.RouteUrl(RouteConfig.NombreRutaBlogPost,
            //    new
            //    {
            //        dia = fechaPost.Day,
            //        mes = fechaPost.Month,
            //        anyo = fechaPost.Year,
            //        urlSlug
            //    }, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string RutaAmigable(this UrlHelper url, string urlSlug)
        {
            return url.RouteUrl(RouteConfig.NombreRutaAmigable,
                new
                {   urlSlug
                }, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string RutaCategoria(this UrlHelper url, string urlCategoria)
        {
            return url.RouteUrl(RouteConfig.NombreRutaCategoriaAmigable,
                new
                {
                    urlCategoria
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
            return url.RouteUrl("Default",
                null, url.RequestContext.HttpContext.Request.Url.Scheme);
        }
    }
}
