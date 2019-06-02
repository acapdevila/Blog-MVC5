
using System;
using System.Collections.Generic;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;
using Infra;

namespace Blog.Servicios.Recetas.Comandos
{
   public  class ComandoEditarReceta
   {
       public ComandoEditarReceta()
       {
           IngredientesAñadidos = new List<ComandoAñadirIngrediente>();
           IngredientesEditados = new List<ComandoEditarIngrediente>();
           IngredientesQuitados = new List<ComandoQuitarIngrediente>();

            InstruccionesAñadidas = new List<ComandoAñadirInstruccion>();
           InstruccionesEditadas= new List<ComandoEditarInstruccion>();
           InstruccionesEliminadas = new List<ComandoEliminarInstruccion>();
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

       public string CocinaReceta { get; set; }

       public string Raciones { get; set; }

       public Imagen Imagen { get; set; }
       
        public IEnumerable<ComandoAñadirIngrediente> IngredientesAñadidos { get; set; }

       public IEnumerable<ComandoEditarIngrediente> IngredientesEditados { get; set; }

        public IEnumerable<ComandoQuitarIngrediente> IngredientesQuitados { get; set; }

       public IEnumerable<ComandoAñadirInstruccion> InstruccionesAñadidas { get; set; }

       public IEnumerable<ComandoEditarInstruccion> InstruccionesEditadas { get; set; }

        public IEnumerable<ComandoEliminarInstruccion> InstruccionesEliminadas { get; set; }
    }
}
