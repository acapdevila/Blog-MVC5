using System;
using System.Collections.Generic;
using Blog.Modelo.Imagenes;

namespace Blog.Modelo.Recetas
{
    public class Receta
    {
        public Receta()
        {
            Imagenes = new List<Imagen>();
        }

        public int Id { get; set; }
        
        public ICollection<Imagen> Imagenes { get; set; }

        public string Nombre { get; set; }

        public string Autor { get; set; }

        public TimeSpan TiempoCoccion { get; set; }

        public DateTime FechaPublicacion { get; set; }
        
        public string Descripcion { get; set; }

        public string Keywords { get; set; }

        public TimeSpan TiempoPreparacion { get; set; }


        public string CategoriaReceta { get; set; }

        public string Raciones { get; set; }


        public TimeSpan TiempoTotal => TiempoPreparacion + TiempoCoccion;

    }
}
