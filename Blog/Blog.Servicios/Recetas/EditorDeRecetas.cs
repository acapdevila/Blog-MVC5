using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Recetas;
using Blog.Servicios.Recetas.Comandos;

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


            await _db.SaveChangesAsync();
        }

        public async Task EliminarReceta(ComandoEliminarReceta comando)
        {
            var receta = await _db.Recetas
                .Include(m => m.Imagenes)
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones)
                .FirstAsync(m => m.Id == comando.IdReceta);


            _db.Recetas.Remove(receta);

            await _db.SaveChangesAsync();
        }

      
    }
}
