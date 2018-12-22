﻿using System;
using System.Collections.Generic;
using Blog.Modelo.Imagenes;

namespace Blog.Modelo.Recetas
{
    public class Receta
    {
        public Receta()
        {
            Imagenes = new List<Imagen>();
            Ingredientes = new List<IngredienteReceta>();
            Instrucciones = new List<Instruccion>();
            FechaPublicacion = DateTime.Today;
        }

        #region Propiedades obligatorias

            public int Id { get; set; }

            public ICollection<Imagen> Imagenes { get; set; }

            public string Nombre { get; set; }


        #endregion

        public string Autor { get; set; }

        public TimeSpan TiempoCoccion { get; set; }

        public DateTime FechaPublicacion { get; set; }
        
        public string Descripcion { get; set; }

        public string Keywords { get; set; }

        public TimeSpan TiempoPreparacion { get; set; }


        public string CategoriaReceta { get; set; }

        public string Raciones { get; set; }


        public TimeSpan TiempoTotal => TiempoPreparacion + TiempoCoccion;

        public ICollection<IngredienteReceta> Ingredientes { get; set; }

        public ICollection<Instruccion> Instrucciones { get; set; }

    }
}
