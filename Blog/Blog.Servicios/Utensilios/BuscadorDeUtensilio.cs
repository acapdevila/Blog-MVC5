using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Utensilios;

namespace Blog.Servicios.Utensilios
{
    public  class BuscadorDeUtensilio
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorDeUtensilio(ContextoBaseDatos db)
        {
            _db = db;
        }

        private IQueryable<Utensilio> Utensilios()
        {
            return _db.Utensilios.Include(m=> m.Categoria);
        }
        
        public async Task<Utensilio> BuscarUtensilioPorIdAsync(int idUtensilio)
        {
            return await Utensilios().FirstOrDefaultAsync(m => m.Id == idUtensilio);
        }

        public async Task<Utensilio> BuscarUtensilioPorNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return null;
            return await Utensilios().FirstOrDefaultAsync(m => m.Nombre == nombre);
        }
    }
}
