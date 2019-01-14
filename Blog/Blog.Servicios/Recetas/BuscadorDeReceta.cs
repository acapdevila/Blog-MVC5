using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Recetas;

namespace Blog.Servicios.Recetas
{
    public  class BuscadorDeReceta
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorDeReceta(ContextoBaseDatos db)
        {
            _db = db;
        }

        private IQueryable<Receta> Recetas()
        {
            return _db.Recetas
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones);
        }
        
        public async Task<Receta> BuscarRecetaPorIdAsync(int idReceta)
        {
            return await Recetas().FirstOrDefaultAsync(m => m.Id == idReceta);
        }

        public async Task<Receta> BuscarRecetaPorNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return null;
            return await Recetas().FirstOrDefaultAsync(m => m.Nombre == nombre);
        }
    }
}
