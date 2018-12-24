using System;
using System.Linq;
using Blog.Modelo.Recetas;

namespace Blog.Servicios.Recetas.Dtos
{
    public  class LineaGestionReceta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public DateTime FechaPublicacion { get; set; }

    }


    public static class ExtensionesLineaGestionReceta
    {
        public static IQueryable<LineaGestionReceta> ProyectarALineaDeReceta(this IQueryable<Receta> recetas)
        {
            return recetas.Select(m => new LineaGestionReceta
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Categoria = m.CategoriaReceta,
                FechaPublicacion = m.FechaPublicacion
            });
        }
    }
}
