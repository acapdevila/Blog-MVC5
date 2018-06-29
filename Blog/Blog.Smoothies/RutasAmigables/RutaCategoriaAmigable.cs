using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Blog.Datos;
using Blog.Smoothies.Controllers;

namespace Blog.Smoothies.RutasAmigables
{
    public class RutaCategoriaAmigableConstraint : IRouteConstraint
    {
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
            
            var buscadorRutas = new BuscadorRutas(new ContextoBaseDatos(), BlogController.TituloBlog);

            return buscadorRutas.ExisteCategoria(urlCategoria.ToLower());

        }
    }
}