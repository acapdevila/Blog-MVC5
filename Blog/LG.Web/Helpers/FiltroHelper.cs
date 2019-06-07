using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Posts;

namespace LG.Web.Helpers
{
    public static class FiltroHelper
    {
        public static List<LineaResumenPost> Impares(this IEnumerable<LineaResumenPost> lista)
        {
            return lista.Where((c, i) => i % 2 != 0).ToList();
        }


        public static List<LineaResumenPost> Pares(this IEnumerable<LineaResumenPost> lista)
        {
            return lista.Where((c, i) => i % 2 == 0).ToList();
        }
    }
}