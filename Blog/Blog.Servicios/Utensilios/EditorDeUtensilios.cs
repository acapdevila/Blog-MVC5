using System.Data.Entity;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Utensilios;
using Blog.Servicios.Utensilios.Comandos;
using Infra;

namespace Blog.Servicios.Utensilios
{
    public class EditorDeUtensilios
    {
        private readonly ContextoBaseDatos _db;

        public EditorDeUtensilios(ContextoBaseDatos db)
        {
            _db = db;
        }


        public async Task CrearUtensilio(ComandoCrearUtensilio comando)
        {
            var categoria = await _db.CategoriasDeUtensilios.FirstOrDefaultAsync(m => m.Nombre == comando.Categoria);

            var utensilio = new Utensilio(
                comando.Nombre, 
                comando.Link,
                new Imagen(comando.ImagenUrl, comando.ImagenAlt), 
                categoria);

            _db.Utensilios.Add(utensilio);

            await _db.SaveChangesAsync();

            comando.Id = utensilio.Id;
        }
        

        public async Task EditarUtensilio(ComandoEditarUtensilio comando)
        {
            var utensilio = await _db.Utensilios
                .FirstAsync(m=>m.Id == comando.Id);

            utensilio.CambiarNombre(comando.Nombre);
            utensilio.CambiarLink(comando.Link);
            utensilio.Imagen = new Imagen(comando.ImagenUrl, comando.ImagenAlt);

            var categoria = await _db.CategoriasDeUtensilios.FirstOrDefaultAsync(m => m.Nombre == comando.Categoria);
            utensilio.Categoria = categoria;

            await _db.SaveChangesAsync();
        }
        

        public async Task EliminarUtensilio(ComandoEliminarUtensilio comando)
        {
            var utensilio = await _db.Utensilios
                .FirstAsync(m => m.Id == comando.IdDeUtensilio);
            
            _db.Utensilios.Remove(utensilio);

            await _db.SaveChangesAsync();
        }
        
        

    }
}
