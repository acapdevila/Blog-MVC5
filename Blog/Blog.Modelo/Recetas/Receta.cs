﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Imagenes;
using Blog.Modelo.Posts;

namespace Blog.Modelo.Recetas
{
    public class Receta
    {
        public Receta()
        {
            Ingredientes = new List<IngredienteReceta>();
            Instrucciones = new List<Instruccion>();
            FechaPublicacion = DateTime.Today;
            Imagen = Imagen.Vacia;
        }

        #region Propiedades obligatorias

            public int Id { get; set; }

            public Imagen Imagen { get; set; }

            public string Nombre { get; set; }
        
            public bool TieneImagen => Imagen != null && Imagen.Url != null;

        #endregion

        public string Autor { get; set; }

        public TimeSpan TiempoCoccion => TimeSpan.FromSeconds(TiempoCoccionEnSegundos);

        public int TiempoCoccionEnSegundos { get; set; }

        public DateTime FechaPublicacion { get; set; }
        
        public string Descripcion { get; set; }

        public string Keywords { get; set; }

        public TimeSpan TiempoPreparacion => TimeSpan.FromSeconds(TiempoPreparacionEnSegundos);

        public int TiempoPreparacionEnSegundos { get; set; }


        public string CategoriaReceta { get; set; }

        public string CocinaReceta { get; set; }

        public string Raciones { get; set; }


        public TimeSpan TiempoTotal => TiempoPreparacion + TiempoCoccion;

        public ICollection<IngredienteReceta> Ingredientes { get; set; }

        public ICollection<Instruccion> Instrucciones { get; set; }

        public Post Post { get; set; }

        public Instruccion ObtenerInstruccion(int posicion)
        {
            return Instrucciones.ElementAt(posicion - 1);
        }

        

        public void AñadirInstruccion(Instruccion instruccion)
        {
            Instrucciones.Add(instruccion);
        }

        public void QuitarInstruccion(Instruccion instruccion)
        {
            Instrucciones.Remove(instruccion);
        }

        public IngredienteReceta ObtenerIngredienteReceta(int posicion)
        {
            return Ingredientes.ElementAt(posicion - 1);
        }

        public void AñadirIngrediente(Ingrediente ingrediete)
        {
           Ingredientes.Add(new IngredienteReceta
           {
               Receta = this,
               Ingrediente = ingrediete
           });
        }

        public void CambiarIngrediente(int posicion, Ingrediente ingrediente)
        {
            ObtenerIngredienteReceta(posicion).Ingrediente = ingrediente;
        }

        public void QuitarIngrediente(IngredienteReceta ingredienteReceta)
        {
            Ingredientes.Remove(ingredienteReceta);
        }

        
      
    }
}
