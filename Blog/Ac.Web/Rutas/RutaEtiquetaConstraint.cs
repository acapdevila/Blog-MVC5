using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Ac.Datos;
using Ac.Servicios.Rutas;
using Infra.Cache;

namespace Blog.Servicios.Rutas
{
    public class RutaEtiquetaConstraint : IRouteConstraint
    {

        private readonly CacheService _cache;


        public RutaEtiquetaConstraint(CacheService cache)
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

            var urlCategoria = Convert.ToString(values[parameterName]);

            if (string.IsNullOrEmpty(urlCategoria)) return false;

            // "^[a-z0-9]+(?:-[a-z0-9]+)*$"
            
            if (!Regex.IsMatch(urlCategoria, @"^[a-zA-Z0-9\-]+$"))
                return false;

            List<RutaDto> rutas = _cache.GetOrAdd(
                CacheSetting.RutasEtiquetas.Key,
                () =>
                {
                    var buscadorRutas = new BuscadorRutas(new ContextoBaseDatos());
                    return buscadorRutas.BuscarRutasDeEtiquetas();
                },
                CacheSetting.RutasEtiquetas.SlidingExpiration);


            return rutas.Any(m => m.UrlSlug.ToLower() == urlCategoria.ToLower());
            
        }
    }
}