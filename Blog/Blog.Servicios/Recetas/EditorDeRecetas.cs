using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Recetas;
using Blog.Servicios.Recetas.Comandos;
using Blog.Servicios.Recetas.Comandos.ComandosIngredientes;
using Blog.Servicios.Recetas.Comandos.ComandosInstrucciones;

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

            AñadirIngredientes(receta, comando.Ingredientes);

            AñadirInstrucciones(receta, comando.Instrucciones);

            _db.Recetas.Add(receta);

            await _db.SaveChangesAsync();
        }

      
        public async Task EditarReceta(ComandoEditarReceta comando)
        {
            var receta = await _db.Recetas
                .Include(m => m.Imagenes)
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

            AñadirIngredientes(receta, comando.IngredientesAñadidos);
            EditarIngredientes(receta, comando.IngredientesEditados);
            QuitarIngredientes(receta, comando.IngredientesQuitados);
            
            AñadirInstrucciones(receta, comando.InstruccionesAñadidas);
            EditarInstrucciones(receta, comando.InstruccionesEditadas);
            EliminarInstrucciones(receta, comando.InstruccionesEliminadas);

            await _db.SaveChangesAsync();
        }
        

        public async Task EliminarReceta(ComandoEliminarReceta comando)
        {
            var receta = await _db.Recetas
                .Include(m => m.Imagenes)
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones)
                .FirstAsync(m => m.Id == comando.IdReceta);
            
            QuitarIngredientes(receta, receta.Ingredientes.Select(m=> new ComandoQuitarIngrediente(m.Id)));
            
            EliminarInstrucciones(receta, receta.Instrucciones.Select(m=> new ComandoEliminarInstruccion(m.Id)));
            
            _db.Recetas.Remove(receta);

            await _db.SaveChangesAsync();
        }



        #region Ingredientes

        private void AñadirIngredientes(Receta receta, IEnumerable<ComandoAñadirIngrediente> comandosCrear)
        {
            foreach (var comandoCrear in comandosCrear)
            {
                var ingrediente = _db.Ingredientes.FirstOrDefault(m => m.Nombre == comandoCrear.Nombre);

                if(ingrediente == null)
                {
                    ingrediente = new Ingrediente(comandoCrear.Nombre);
                    _db.Ingredientes.Add(ingrediente);
                }

                receta.AñadirIngrediente(ingrediente);
            }
        }

        private void EditarIngredientes(Receta receta, IEnumerable<ComandoEditarIngrediente> comandoIngredientesEditados)
        {
            foreach (var comandoEditar in comandoIngredientesEditados)
            {
                var ingredienteReceta = receta.BuscarIngredienteRecetaPorId(comandoEditar.IdIngredienteReceta);

                if(ingredienteReceta == null) continue;

                // no hay cambios
                if (ingredienteReceta.Ingrediente.Nombre == comandoEditar.Nombre) continue;

                var ingrediente = _db.Ingredientes.FirstOrDefault(m => m.Nombre == comandoEditar.Nombre);

                if (ingrediente == null)
                {
                    ingrediente = new Ingrediente(comandoEditar.Nombre);
                    _db.Ingredientes.Add(ingrediente);
                }

                ingredienteReceta.Ingrediente = ingrediente;
             }
        }


        private void QuitarIngredientes(Receta receta, IEnumerable<ComandoQuitarIngrediente> comandosQuitar)
        {
            foreach (var comandoQuitar in comandosQuitar)
            {
                var ingrediente = receta.BuscarIngredienteRecetaPorId(comandoQuitar.Id);

                if(ingrediente == null) continue;

                receta.QuitarIngrediente(ingrediente);

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
                var instruccion = receta.BuscarInstruccionPorId(comandoEditarInstruccion.Id);
                
                if (instruccion == null) continue;

                instruccion.Nombre = comandoEditarInstruccion.Nombre;
            }
        }

        private void EliminarInstrucciones(Receta receta, IEnumerable<ComandoEliminarInstruccion> comandoInstruccionesEliminadas)
        {
            foreach (var comandoEliminarInstruccion in comandoInstruccionesEliminadas)
            {
                var instruccion = receta.BuscarInstruccionPorId(comandoEliminarInstruccion.Id);
                receta.QuitarInstruccion(instruccion);
                _db.InstruccionesDeRecetas.Remove(instruccion);
            }
        }


        #endregion


    }
}
