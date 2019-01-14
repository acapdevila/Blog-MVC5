using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
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
        
        public async Task<IPagedList<LineaGestionReceta>> BuscarPaginaAsync(CriteriosBusquedaReceta criterios, int indicePagina, int tamañoPagina)
        {
            var consulta = _db.Recetas
                .ProyectarALineaDeReceta();

            if (!string.IsNullOrEmpty(criterios.BuscarPor))
            {
                consulta = consulta.Where(m => criterios.BuscarPor.Contains(m.Nombre) || m.Nombre.Contains(criterios.BuscarPor) ||
                                               criterios.BuscarPor.Contains(m.Categoria) || m.Categoria.Contains(criterios.BuscarPor) );
            }

            return await consulta
                        .OrderBy(m=>m.Nombre)
                        .ToPagedListAsync(indicePagina, tamañoPagina);

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
