using System;
using System.Collections.Generic;
using Blog.Modelo.Recetas;

namespace Blog.Smoothies.Views.Recetas.ViewModels
{
    public  class EditorRecetaPartialModel
    {
        public EditorRecetaPartialModel() : this(new Receta())
        {
            Instrucciones = new List<EditorInstruccionPartialModel>();
        }

        public EditorRecetaPartialModel(Receta receta) 
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

            foreach (var instruccion in receta.Instrucciones)
            {
                Instrucciones.Add(new EditorInstruccionPartialModel(instruccion));
            }

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

        public  List<EditorInstruccionPartialModel> Instrucciones { get; set; }
        
    }
}
