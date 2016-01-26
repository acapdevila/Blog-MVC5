using System;
using System.Web.Mvc;

namespace Blog.Web.Helpers
{
    public static class UrlHelperExtension
    {
        public static string RutaUrlBlogPost(this UrlHelper url, DateTime fechaPost, string urlSlug)
        {
            return url.RouteUrl("BlogPost",
                new
                {
                    dia = fechaPost.Day,
                    mes = fechaPost.Month,
                    anyo = fechaPost.Year,
                    urlSlug
                });
        }
    }
}
