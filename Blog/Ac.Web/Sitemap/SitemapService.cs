using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Ac.Infra.Cache;
using Ac.Web.Rutas;

namespace Ac.Web.Sitemap
{
    /// <summary>
    /// Generates sitemap XML for the current site.
    /// </summary>
    public class SitemapService : SitemapGenerator
    {
        #region Fields

        private readonly BuscadorRutas _buscadorRutas;
        private readonly UrlHelper _urlHelper;
        private readonly CacheService _cache;

        #endregion

        #region Constructors

        public SitemapService(
            UrlHelper urlHelper, BuscadorRutas buscadorRutas, CacheService cache)
        {
            _urlHelper = urlHelper;
            _buscadorRutas = buscadorRutas;
            _cache = cache;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the sitemap XML for the current site. If an index of null is passed and there are more than 25,000 
        /// sitemap nodes, a sitemap index file is returned (A sitemap index file contains links to other sitemap files 
        /// and is a way of splitting up your sitemap into separate files). If an index is specified, a standard 
        /// sitemap is returned for the specified index parameter. See http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root 
        /// sitemap or sitemap index document, depending on the number of sitemap nodes.</param>
        /// <returns>The sitemap XML for the current site or <c>null</c> if the sitemap index is out of range.</returns>
        public string GetSitemapXml(int? index = null)
        {
            // Here we are caching the entire set of sitemap documents. We cannot use OutputCacheAttribute because 
            // cache expiry could get out of sync if the number of sitemaps changes.
            List<string> sitemapDocuments = _cache.GetOrAdd(
                CacheSetting.SitemapNodes.Key,
                () =>
                {
                    IReadOnlyCollection<SitemapNode> sitemapNodes = this.GetSitemapNodes();
                    return this.GetSitemapDocuments(sitemapNodes);
                },
                CacheSetting.SitemapNodes.SlidingExpiration);

            if (index.HasValue && ((index < 1) || (index.Value >= sitemapDocuments.Count)))
            {
                return null;
            }

            return sitemapDocuments[index.HasValue ? index.Value : 0];
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a collection of sitemap nodes for the current site.
        /// TODO: Add code here to create nodes to all your important sitemap URL's.
        /// You may want to do this from a database or in code.
        /// </summary>
        /// <returns>A collection of sitemap nodes for the current site.</returns>
        protected virtual IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();


            // Portada
            nodes.Add(
                new SitemapNode(this._urlHelper.AbsoluteAction("", ""))
                {
                    Priority = 1
                });

         

            // Blog
            nodes.Add(
                new SitemapNode(this._urlHelper.AbsoluteAction("Index", "Blog"))
                {
                    Frequency = SitemapFrequency.Monthly,
                    Priority = 1
                });

            nodes.Add(
                new SitemapNode(this._urlHelper.AbsoluteAction("Index", "Contratame"))
                {
                    Priority = 1
                });


            // An example of how to add many pages into your sitemap.
            foreach (var ruta in _buscadorRutas.BuscarRutasDeEtiquetas())
            {
                nodes.Add(
                    new SitemapNode(_urlHelper.AbsoluteRouteUrl(RouteConfig.NombreRutaEtiquetaAmigable, new { urlEtiqueta = ruta.UrlSlug }))
                    {
                       // Frequency = SitemapFrequency.Weekly,
                        LastModified = ruta.FechaPublicacion,
                        Priority = 0.9
                    });
            }

            // An example of how to add many pages into your sitemap.
            foreach (var ruta in _buscadorRutas.BuscarRutasDePosts())
            {
                var url = _urlHelper.AbsoluteRouteUrl(RouteConfig.NombreRutaAmigable, new {urlSlug = ruta.UrlSlug});

                if(string.IsNullOrEmpty(url)) continue;

                nodes.Add(new SitemapNode(url)
                   {
                       LastModified = ruta.FechaPublicacion,
                       Priority = 0.9
                   });
            }
            
            nodes.Add(
                    new SitemapNode(this._urlHelper.AbsoluteAction("Index", "Libros"))
                    {
                        Priority = 0.5
                    });
            
         
            
            nodes.Add(
                new SitemapNode(this._urlHelper.AbsoluteAction("Index", "Contacto"))
                {
                    Priority = 0.5
                });

            return nodes;
        }

        protected override string GetSitemapUrl(int index)
        {
            return _urlHelper.AbsoluteRouteUrl(RouteConfig.NombreRutaSitemap).TrimEnd('/') + "?index=" + index;
        }

        protected override void LogWarning(Exception exception)
        {
            Trace.TraceError(exception.ToString());
            //this.loggingService.Log(exception);
        }

        #endregion
    }
}