using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Utensilios;

namespace Blog.Servicios.Utensilios
{
    public  class BuscadorDeCategoriaDeUtensilio
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorDeCategoriaDeUtensilio(ContextoBaseDatos db)
        {
            _db = db;
        }

        private IQueryable<UtensilioCategoria> Categorias()
        {
            return _db.CategoriasDeUtensilios;
        }
        
        public async Task<UtensilioCategoria> BuscarCategoriaPorIdAsync(int idCategoria)
        {
            return await Categorias().FirstOrDefaultAsync(m => m.Id == idCategoria);
        }

        public async Task<UtensilioCategoria> BuscarCategoriaPorNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return null;
            return await Categorias().FirstOrDefaultAsync(m => m.Nombre == nombre);
        }
    }
}
