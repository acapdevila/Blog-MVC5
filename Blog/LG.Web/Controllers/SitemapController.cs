﻿using System.Net;
using System.Text;
using System.Web.Mvc;
using Blog.Datos;
using Infra.Cache;
using LG.Web.Rutas;
using LG.Web.Sitemap;

namespace LG.Web.Controllers
{
    public class SitemapController : Controller
    {
        private readonly SitemapService _sitemapService;

        public SitemapController() : this(
                new SitemapService(
                    new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext), 
                    new BuscadorRutas(new ContextoBaseDatos(), BlogController.TituloBlog),
                    new CacheService()))
        {
        }

        public SitemapController(SitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }

        [NoTrailingSlash]
        public ActionResult Xml(int? index = null)
        {
            //var _sitemapService = new SitemapService(
            //    new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext),
            //    new BuscadorRutas(new ContextoBaseDatos(), BlogController.TituloBlog),
            //    new CacheService());
            
            string content = _sitemapService.GetSitemapXml(index);

            //var dataFile = Server.MapPath("~/sitemap.xml");
            //System.IO.File.AppendAllText(@dataFile, content);


            if (content == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Sitemap index is out of range.");
            }

            return this.Content(content, "application/xml", Encoding.UTF8);
        }



    }
}