using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;

namespace Blog.Smoothies.Views.Recetas.ViewModels.Editores
{
    public  class EditorReceta
    {
        public EditorReceta() : this(new Receta())
        {
         
        }

        public EditorReceta(Receta receta) 
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

            Instrucciones = new List<EditorInstruccion>();

            foreach (var instruccion in receta.Instrucciones)
            {
                Instrucciones.Add(new EditorInstruccion(instruccion));
            }

            if(!Instrucciones.Any()) Instrucciones.Add(new EditorInstruccion());


            Ingredientes = new List<EditorIngredienteReceta>();

            foreach (var ingredienteReceta in receta.Ingredientes)
            {
                Ingredientes.Add(new EditorIngredienteReceta(ingredienteReceta));
            }

            if (!Ingredientes.Any()) Ingredientes.Add(new EditorIngredienteReceta());


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

        public  List<EditorInstruccion> Instrucciones { get; set; }

        public List<EditorIngredienteReceta> Ingredientes { get; set; }

       
        public ComandoEditarReceta GenerarComandoEditarReceta()
        {
            return new ComandoEditarReceta
            {
                Id = Id,
                Autor = Autor,
                Nombre = Nombre,
                CategoriaReceta = CategoriaReceta,
                Descripcion = Descripcion,
                FechaPublicacion = FechaPublicacion,
                Keywords = Keywords,
                Raciones = Raciones,
                TiempoCoccion = TiempoCoccion,
                TiempoPreparacion = TiempoPreparacion,
                IngredientesAñadidos = ObtenerIngredientesAñadidos().Select(m => new ComandoAñadirIngrediente(m.NombreIngrediente)),
                IngredientesEditados = ObtenerIngredientesEditados().Select(m=> new ComandoEditarIngrediente(m.IdIngredienteReceta, m.NombreIngrediente)),
                IngredientesQuitados = ObtenerIngredientesEliminados().Select(m => new ComandoQuitarIngrediente(m.IdIngredienteReceta)),
                InstruccionesAñadidas = ObtenerInstruccionesAñadidas().Select(m => new ComandoAñadirInstruccion(m.Nombre)),
                InstruccionesEditadas = ObtenerInstruccionesEditadas().Select(m => new ComandoEditarInstruccion(m.Id, m.Nombre)),
                InstruccionesEliminadas = ObtenerInstruccionesEliminadas().Select(m => new ComandoEliminarInstruccion(m.Id))
            };
        }

        public ComandoCrearReceta GenerarComandoCrearReceta()
        {
            return new ComandoCrearReceta
            {
                Autor = Autor,
                Nombre = Nombre,
                CategoriaReceta = CategoriaReceta,
                Descripcion = Descripcion,
                FechaPublicacion = FechaPublicacion,
                Keywords = Keywords,
                Raciones = Raciones,
                TiempoCoccion = TiempoCoccion,
                TiempoPreparacion = TiempoPreparacion,
                Instrucciones = ObtenerInstruccionesAñadidas().Select(m => new ComandoAñadirInstruccion(m.Nombre))
            };
        }


        private IEnumerable<EditorIngredienteReceta> ObtenerIngredientesAñadidos()
        { return Ingredientes.Where(m => m.IdIngredienteReceta <= 0).ToList(); }

        private IEnumerable<EditorIngredienteReceta> ObtenerIngredientesEditados()
        { return Ingredientes.Where(m => !m.EstaMarcadoParaEliminar && 0 < m.IdIngredienteReceta).ToList(); }


        private IEnumerable<EditorIngredienteReceta> ObtenerIngredientesEliminados()
        { return Ingredientes.Where(m => m.EstaMarcadoParaEliminar && 0 < m.IdIngredienteReceta).ToList(); }





        private IEnumerable<EditorInstruccion> ObtenerInstruccionesAñadidas()
        { return Instrucciones.Where(m => m.Id <= 0).ToList(); }

        private IEnumerable<EditorInstruccion> ObtenerInstruccionesEditadas()
        { return Instrucciones.Where(m => !m.EstaMarcadoParaEliminar && 0 < m.Id).ToList(); }

        private IEnumerable<EditorInstruccion> ObtenerInstruccionesEliminadas()
        { return Instrucciones.Where(m => m.EstaMarcadoParaEliminar && 0 < m.Id).ToList(); }

      
      

    }
}
