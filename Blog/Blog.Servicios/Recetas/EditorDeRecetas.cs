using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Imagenes;
using Blog.Modelo.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;
using Blog.Servicios.Recetas.Comandos.Imagenes;

namespace Blog.Servicios.Recetas
{
    public class EditorDeRecetas
    {
        private readonly ContextoBaseDatos _db;

        public EditorDeRecetas(ContextoBaseDatos db)
        {
            _db = db;
        }


        public async Task CrearReceta(ComandoCrearReceta comando)
        {
            var receta = new Receta
            {
                Nombre = comando.Nombre,
                CategoriaReceta = comando.CategoriaReceta,
                Autor = comando.Autor,
                Keywords = comando.Keywords,
                FechaPublicacion = comando.FechaPublicacion,
                Descripcion = comando.Descripcion,
                Raciones = comando.Raciones,
                TiempoCoccion = comando.TiempoCoccion,
                TiempoPreparacion = comando.TiempoPreparacion
            };

            await AñadirIngredientes(receta, comando.Ingredientes);

            AñadirInstrucciones(receta, comando.Instrucciones);
            
            _db.Recetas.Add(receta);

            await _db.SaveChangesAsync();
        }
        

        public async Task EditarReceta(ComandoEditarReceta comando)
        {
            var receta = await _db.Recetas
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones)
                .FirstAsync(m=>m.Id == comando.Id);

            receta.Nombre = comando.Nombre;
            receta.CategoriaReceta = comando.CategoriaReceta;
            receta.Autor = comando.Autor;
            receta.Keywords = comando.Keywords;
            receta.FechaPublicacion = comando.FechaPublicacion;
            receta.Descripcion = comando.Descripcion;
            receta.Raciones = comando.Raciones;
            receta.TiempoCoccion = comando.TiempoCoccion;
            receta.TiempoPreparacion = comando.TiempoPreparacion;

            await AñadirIngredientes(receta, comando.IngredientesAñadidos);
            await EditarIngredientes(receta, comando.IngredientesEditados);
            QuitarIngredientes(receta, comando.IngredientesQuitados);
            
            AñadirInstrucciones(receta, comando.InstruccionesAñadidas);
            EditarInstrucciones(receta, comando.InstruccionesEditadas);
            EliminarInstrucciones(receta, comando.InstruccionesEliminadas);

            receta.Imagen = comando.Imagen;

            await _db.SaveChangesAsync();
        }
        

        public async Task EliminarReceta(ComandoEliminarReceta comando)
        {
            var receta = await _db.Recetas
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones)
                .FirstAsync(m => m.Id == comando.IdReceta);
            
            QuitarIngredientes(receta, receta.Ingredientes.Select(m=> new ComandoQuitarIngrediente(m.Id)));
            
            EliminarInstrucciones(receta, receta.Instrucciones.Select(m=> new ComandoEliminarInstruccion(m.Id)));
            
            _db.Recetas.Remove(receta);

            await _db.SaveChangesAsync();
        }
        

        #region Ingredientes

        private async Task<Ingrediente> BuscarIngredientePorNombreAsync(string nombreIngrediente)
        {
            return await _db.Ingredientes.FirstOrDefaultAsync(m => m.Nombre == nombreIngrediente);
        }

        private Ingrediente CrearIngredientePorNombre(string nombreIngrediente)
        {
            var ingrediente = new Ingrediente(nombreIngrediente);
            _db.Ingredientes.Add(ingrediente);
            return ingrediente;
        }

        private async  Task AñadirIngredientes(Receta receta, IEnumerable<ComandoAñadirIngrediente> comandosCrear)
        {
            foreach (var comandoCrear in comandosCrear)
            {
                var ingrediente = await BuscarIngredientePorNombreAsync(comandoCrear.Nombre) ??
                                        CrearIngredientePorNombre(comandoCrear.Nombre);

                receta.AñadirIngrediente(ingrediente);
            }
        }

        private async Task EditarIngredientes(Receta receta, IEnumerable<ComandoEditarIngrediente> comandoIngredientesEditados)
        {
            foreach (var comandoEditar in comandoIngredientesEditados)
            {
                var ingredienteReceta = receta.ObtenerIngredienteReceta(comandoEditar.Posicion);
                
                // no hay cambios
                if (ingredienteReceta.Nombre == comandoEditar.Nombre) continue;

                var ingrediente = await BuscarIngredientePorNombreAsync(comandoEditar.Nombre) ??
                                        CrearIngredientePorNombre(comandoEditar.Nombre);

                receta.CambiarIngrediente(comandoEditar.Posicion, ingrediente);
             }
        }
        

        private void QuitarIngredientes(Receta receta, IEnumerable<ComandoQuitarIngrediente> comandosQuitar)
        {
            var ingredientesAEliminar = comandosQuitar
                .Select(comando => receta.ObtenerIngredienteReceta(comando.Posicion))
                .ToList();

            while (ingredientesAEliminar.Any())
            {
                var primero = ingredientesAEliminar.First();
                receta.QuitarIngrediente(primero);
                _db.IngredientesDeRecetas.Remove(primero);
                ingredientesAEliminar.Remove(primero);
            }
        }


        #endregion


        #region Instrucciones

        private void AñadirInstrucciones(Receta receta, IEnumerable<ComandoAñadirInstruccion> comandosCrearInstrucciones)
        {
            foreach (var comandoCrearInstruccion in comandosCrearInstrucciones)
            {
                var instruccion = new Instruccion(comandoCrearInstruccion.Nombre);
                receta.AñadirInstruccion(instruccion);
            }
        }

        private void EditarInstrucciones(Receta receta, IEnumerable<ComandoEditarInstruccion> comandoInstruccionesEditadas)
        {
            foreach (var comandoEditarInstruccion in comandoInstruccionesEditadas)
            {
                var instruccion = receta.ObtenerInstruccion(comandoEditarInstruccion.Posicion);
                
                if (instruccion.Nombre == comandoEditarInstruccion.Nombre) continue;

                instruccion.Nombre = comandoEditarInstruccion.Nombre;
            }
        }

        private void EliminarInstrucciones(Receta receta, IEnumerable<ComandoEliminarInstruccion> comandoInstruccionesEliminadas)
        {
            var instruccionesAEliminar = comandoInstruccionesEliminadas
                        .Select(comando => receta.ObtenerInstruccion(comando.Posicion))
                        .ToList();

            while (instruccionesAEliminar.Any())
            {
                var primeraInstruccion = instruccionesAEliminar.First();
                receta.QuitarInstruccion(primeraInstruccion);
                _db.InstruccionesDeRecetas.Remove(primeraInstruccion);
                instruccionesAEliminar.Remove(primeraInstruccion);
            }
        }


        #endregion


    }
}
