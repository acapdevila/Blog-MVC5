using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class VistaPreviaRecetaViewModel
    {
        public VistaPreviaRecetaViewModel(Receta receta)
        {
            Id = receta.Id;
            Nombre = receta.Nombre;
            Autor = receta.Autor;
            CategoriaReceta = receta.CategoriaReceta;
            FechaPublicacion = receta.FechaPublicacion;
            Descripcion = receta.Descripcion;
            Keywords = receta.Keywords;
            Raciones = receta.Raciones;
            TiempoCoccion = receta.TiempoCoccion;
            TiempoPreparacion = receta.TiempoPreparacion;

            Imagenes = receta.Imagenes.Select(m => m.Url);
            Ingredientes = receta.Ingredientes.Select(m => m.Nombre);
            Instrucciones = receta.Instrucciones.Select(m => m.Nombre);
        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Autor { get; set; }

        public TimeSpan TiempoCoccion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string Descripcion { get; set; }

        public string Keywords { get; set; }

        public TimeSpan TiempoPreparacion { get; set; }

        public string CategoriaReceta { get; set; }

        public string Raciones { get; set; }

        public IEnumerable<string> Imagenes { get; }
        public IEnumerable<string> Instrucciones { get; }

        public IEnumerable<string> Ingredientes { get; }

        public TimeSpan TiempoTotal => TiempoPreparacion + TiempoCoccion;
    }
}
