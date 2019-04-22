using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Blog.Modelo.Imagenes;
using Blog.Modelo.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;
using WebGrease.Css.Extensions;

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

            AltImagen =receta.Imagen.Alt;
            UrlImagen = receta.Imagen.Url;

            AñadirInstrucciones(receta);

            AñadirIngredientes(receta);
        }



        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Autor { get; set; }

        [Display(Name = "Alt imagen")]
        public string AltImagen { get; set; }

        [Display(Name = "Url imagen")]
        public string UrlImagen { get; set; }
        
        [Display(Name = "Tiempo de cocción")]
        public TimeSpan TiempoCoccion { get; set; }

        [Display(Name = "Tiempo de preparación")]
        public TimeSpan TiempoPreparacion { get; set; }

        [Display(Name = "Publicación")]
        public DateTime FechaPublicacion { get; set; }


        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Keywords (,)")]
        public string Keywords { get; set; }


        [Display(Name = "Categoría")]
        public string CategoriaReceta { get; set; }

        public string Raciones { get; set; }

        public  List<EditorInstruccion> Instrucciones { get; set; }

        public List<EditorIngredienteReceta> Ingredientes { get; set; }
        public bool TieneImagen => !string.IsNullOrEmpty(UrlImagen);

        public string AccionPost { get; set; }
        public string AccionQuitarImagen { get; set; }
        public string AccionSubirImagen { get; set; }


        #region Métodos privados


        private void AñadirIngredientes(Receta receta)
        {
            Ingredientes = new List<EditorIngredienteReceta>();

            receta.Ingredientes.ForEach((ingrediente, indice)
                => Ingredientes.Add(new EditorIngredienteReceta(indice + 1, ingrediente)));

            if (!Ingredientes.Any()) Ingredientes.Add(new EditorIngredienteReceta());
        }

        private void AñadirInstrucciones(Receta receta)
        {
            Instrucciones = new List<EditorInstruccion>();

            receta.Instrucciones.ForEach((instruccion, indice)
                => Instrucciones.Add(new EditorInstruccion(indice + 1, instruccion)));

            if (!Instrucciones.Any()) Instrucciones.Add(new EditorInstruccion());
        }

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
                IngredientesAñadidos = ObtenerComandosAñadirIngredientes(),
                IngredientesEditados = ObtenerComandosEditarIngredientes(),
                IngredientesQuitados = ObtenerComandosEliminarIngredientes(),
                InstruccionesAñadidas = ObtenerComandosAñadirInstrucciones(),
                InstruccionesEditadas = ObtenerComandosEditarInstrucciones(),
                InstruccionesEliminadas = ObtenerComandosEliminarInstrucciones(),
                Imagen = ObtenerImagen() 
            };
        }

   


        public ComandoCrearReceta GenerarComandoCrearReceta()
        {
            var comando= new ComandoCrearReceta
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
                Ingredientes = ObtenerComandosAñadirIngredientes(),
                Instrucciones = ObtenerComandosAñadirInstrucciones(),
                Imagen = ObtenerImagen()
            };

                

            return comando;
        }

        
        private IEnumerable<ComandoAñadirIngrediente> ObtenerComandosAñadirIngredientes()
        { return Ingredientes.Where(m => !m.EstaMarcadoParaEliminar && m.Posicion <= 0).ToList()
            .Select(i => new ComandoAñadirIngrediente(i.NombreIngrediente)); }

        private IEnumerable<ComandoEditarIngrediente> ObtenerComandosEditarIngredientes()
        { return Ingredientes.Where(m => !m.EstaMarcadoParaEliminar && 0 < m.Posicion).ToList()
                .Select(m => new ComandoEditarIngrediente(m.Posicion, m.NombreIngrediente)); }


        private IEnumerable<ComandoQuitarIngrediente> ObtenerComandosEliminarIngredientes()
        { return Ingredientes.Where(m => m.EstaMarcadoParaEliminar && 0 < m.Posicion).ToList()
            .Select(m => new ComandoQuitarIngrediente(m.Posicion)); }

        
        private IEnumerable<ComandoAñadirInstruccion> ObtenerComandosAñadirInstrucciones()
        { return Instrucciones.Where(m => !m.EstaMarcadoParaEliminar && m.Posicion <= 0).ToList()
            .Select(m => new ComandoAñadirInstruccion(m.Nombre)); }

        private IEnumerable<ComandoEditarInstruccion> ObtenerComandosEditarInstrucciones()
        { return Instrucciones.Where(m => !m.EstaMarcadoParaEliminar && 0 < m.Posicion).ToList()
                    .Select(m => new ComandoEditarInstruccion(m.Posicion, m.Nombre)); }

        private IEnumerable<ComandoEliminarInstruccion> ObtenerComandosEliminarInstrucciones()
        {
            return Instrucciones.Where(m => m.EstaMarcadoParaEliminar && 0 < m.Posicion).ToList()
                .Select(m => new ComandoEliminarInstruccion(m.Posicion));
        }

        private Imagen ObtenerImagen()
        {
            return new Imagen(UrlImagen, AltImagen ?? Nombre);
        }



        #endregion

    }
}
