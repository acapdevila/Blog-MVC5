using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Blog.Datos;
using Blog.Web.Controllers;

namespace Blog.Web.RutasAmigables
{
    public class RutaEtiquetaAmigableConstraint : IRouteConstraint
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

            var urlEtiqueta = Convert.ToString(values[parameterName]);

            if (string.IsNullOrEmpty(urlEtiqueta)) return false;

            // "^[a-z0-9]+(?:-[a-z0-9]+)*$"
            
            if (!Regex.IsMatch(urlEtiqueta, @"^[a-zA-Z0-9\-]+$"))
                return false;
            
            var buscadorRutas = new BuscadorRutas(new ContextoBaseDatos(), BlogController.TituloBlog);

            return buscadorRutas.ExisteEtiqueta(urlEtiqueta.ToLower());

        }
    }
}