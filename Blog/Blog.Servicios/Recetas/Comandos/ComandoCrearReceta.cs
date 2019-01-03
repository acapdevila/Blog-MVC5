using System;
using System.Collections.Generic;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;

namespace Blog.Servicios.Recetas.Comandos
{
    public class ComandoCrearReceta
    {
        public ComandoCrearReceta()
        {
            Instrucciones = new List<ComandoAñadirInstruccion>();
        }

        public string Nombre { get; set; }

        public string Autor { get; set; }

        public TimeSpan TiempoCoccion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string Descripcion { get; set; }

        public string Keywords { get; set; }

        public TimeSpan TiempoPreparacion { get; set; }

        public string CategoriaReceta { get; set; }

        public string Raciones { get; set; }

        public IEnumerable<ComandoAñadirIngrediente> Ingredientes { get; set; }

        public IEnumerable<ComandoAñadirInstruccion> Instrucciones { get; set; }

        
    }
}
