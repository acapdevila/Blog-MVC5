using System.Linq;
using Blog.Modelo.Utensilios;

namespace Blog.Servicios.Utensilios.Dtos
{
    public  class LineaGestionDeUtensilio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Categoria { get; set; }
     }


    public static class ExtensionesLineaGestionDeUtensilio
    {
        public static IQueryable<LineaGestionDeUtensilio> ProyectarALineaDeUtensilio(this IQueryable<Utensilio> utensilios)
        {
            return utensilios.Select(m => new LineaGestionDeUtensilio
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Categoria = m.Categoria.Nombre
            });
        }
    }
}
