using System.Linq;
using Blog.Modelo.Utensilios;

namespace Blog.Servicios.Utensilios.Dtos
{
    public  class LineaGestionCategoriaDeUtensilio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
     }


    public static class ExtensionesLineaGestionCategoriaDeUtensilio
    {
        public static IQueryable<LineaGestionCategoriaDeUtensilio> ProyectarALineaDeCategoria(this IQueryable<UtensilioCategoria> categorias)
        {
            return categorias.Select(m => new LineaGestionCategoriaDeUtensilio
            {
                Id = m.Id,
                Nombre = m.Nombre
            });
        }
    }
}
