using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Recetas;
using Blog.Servicios.Comun;
using Blog.Servicios.Recetas.Dtos;
using PagedList;
using PagedList.EntityFramework;

namespace Blog.Servicios.Recetas
{
    public  class BuscadorDeRecetas
    {
        private readonly ContextoBaseDatos _db;

        public BuscadorDeRecetas(ContextoBaseDatos db)
        {
            _db = db;
        }

        private IQueryable<Receta> Recetas()
        {
            return _db.Recetas
                .Include(m => m.Imagenes)
                .Include(m => m.Ingredientes.Select(i => i.Ingrediente))
                .Include(m => m.Instrucciones);
        }

        public async Task<IPagedList<LineaGestionReceta>> BuscarPaginaAsync(CriteriosBusquedaReceta criterios, int indicePagina, int tamañoPagina)
        {
            return await Recetas()
                    .ProyectarALineaDeReceta()
                    .Where(m=> criterios.BuscarPor.Contains(m.Nombre))
                    .OrderBy(m=>m.Nombre)
                    .ToPagedListAsync(indicePagina, tamañoPagina);

        }

        public async Task<Receta> BuscarRecetaPorId(int idReceta)
        {
            return await Recetas().FirstOrDefaultAsync(m => m.Id == idReceta);
        }

        public async Task<List<ElementoValorDescripcion>> ElementosDeRecetas(string contiene = null, int maximoElementos = 10)
        {
            return await _db.Recetas
                    .Where(m => m.Nombre.Contains(contiene))
                    .OrderBy(m => m.Nombre)
                    .Take(maximoElementos)
                    .Select(m=> new ElementoValorDescripcion
                {
                    Valor = m.Id,
                    Descripcion = m.Nombre
                })
                    .ToListAsync();

        }
    }
}
