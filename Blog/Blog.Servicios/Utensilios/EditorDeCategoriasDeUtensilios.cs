using System.Data.Entity;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Utensilios;
using Blog.Servicios.Utensilios.Comandos;

namespace Blog.Servicios.Utensilios
{
    public class EditorDeCategoriasDeUtensilios
    {
        private readonly ContextoBaseDatos _db;

        public EditorDeCategoriasDeUtensilios(ContextoBaseDatos db)
        {
            _db = db;
        }


        public async Task CrearCategoria(ComandoCrearCategoriaUtensilio comando)
        {
            var categoria = new UtensilioCategoria(comando.Nombre, comando.UrlSlug, comando.Posicion);
            
            _db.CategoriasDeUtensilios.Add(categoria);

            await _db.SaveChangesAsync();

            comando.Id = categoria.Id;
        }
        

        public async Task EditarCategoria(ComandoEditarCategoriaUtensilio comando)
        {
            var categoria = await _db.CategoriasDeUtensilios
                .FirstAsync(m=>m.Id == comando.Id);

            categoria.Nombre = comando.Nombre;
            categoria.UrlSlug = comando.UrlSlug;
            categoria.Posicion = comando.Posicion;

            await _db.SaveChangesAsync();
        }
        

        public async Task EliminarCategoria(ComandoEliminarCategoriaUtensilio comando)
        {
            var categoria = await _db.CategoriasDeUtensilios
                .FirstAsync(m => m.Id == comando.IdDeCategoriaDeUtensilio);
            
            _db.CategoriasDeUtensilios.Remove(categoria);

            await _db.SaveChangesAsync();
        }
        
        

    }
}
