using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Ac.Datos;
using Ac.Infra.Cache;

namespace Ac.Web.Rutas
{
    public class RutaPostConstraint : IRouteConstraint
    {

        private readonly CacheService _cache;

        public RutaPostConstraint(CacheService cache)
        {
            _cache = cache;
        }
        
        public bool Match(
            HttpContextBase httpContext, 
            Route route,
            string parameterName, 
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return false;

            var url = Convert.ToString(values[parameterName]);

            if (string.IsNullOrEmpty(url)) return false;

            // "^[a-z0-9]+(?:-[a-z0-9]+)*$"
            
            if (!Regex.IsMatch(url, @"^[a-zA-Z0-9\-]+$"))
                return false;
            
            List<RutaDto> rutas = _cache.GetOrAdd(
                CacheSetting.RutasPosts.Key,
                () =>
                {
                      var buscadorRutas = new BuscadorRutas(new ContextoBaseDatos());
                      return buscadorRutas.BuscarRutasDePosts();
                },
                CacheSetting.RutasPosts.SlidingExpiration);
            
            return rutas.Any(m=>m.UrlSlug.ToLower() == url.ToLower());

        }
    }


}