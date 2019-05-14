using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Utensilios;
using Blog.Servicios.Comun;

namespace Blog.Servicios.Blog
{
    public class BuscadorPostsUtensilios
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorPostsUtensilios(ContextoBaseDatos db)
        {
            _db = db;
        }

        public async Task<List<Utensilio>> BuscarPostsUtensiliosPorNombresAsync(List<string> nombres)
        {
            if(!nombres.Any()) return new List<Utensilio>();

            var utensilios = await _db.Utensilios.Where(m => nombres.Any(t => t.ToLower() == m.Nombre.ToLower())).ToListAsync();

            if (!utensilios.Any()) return utensilios;

            var utensiliosOrdenados = new List<Utensilio>();

            foreach (var titulo in nombres)
            {
                var utensilioEncontrado = utensilios.FirstOrDefault(m => m.Nombre.ToLower() == titulo.ToLower());
                if (utensilioEncontrado != null) 
                    utensiliosOrdenados.Add(utensilioEncontrado);
            }

            return utensiliosOrdenados;
        }
        
        public async Task<List<ElementoValorDescripcion>> NombresDeUtensilios(string contiene = null, int maximoElementos = 10)
        {
            return await _db.Utensilios
                .Where(m => m.Nombre.Contains(contiene))
                .OrderBy(m => m.Nombre)
                .Take(maximoElementos)
                .Select(m => new ElementoValorDescripcion
                {
                    Valor = m.Id,
                    Descripcion = m.Nombre
                })
                .ToListAsync();

        }
    }
}
