using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.Servicios.Comun;
using Blog.Servicios.Utensilios.Dtos;
using PagedList;
using PagedList.EntityFramework;

namespace Blog.Servicios.Utensilios
{
    public  class BuscadorDeCategoriasDeUtensilios
    {
        private readonly ContextoBaseDatos _db;
        
        public BuscadorDeCategoriasDeUtensilios(ContextoBaseDatos db)
        {
            _db = db;
        }
        
        public async Task<IPagedList<LineaGestionCategoriaDeUtensilio>> BuscarPaginaAsync(CriteriosBusqueda criterios, int indicePagina, int tamañoPagina)
        {
            var consulta = _db.CategoriasDeUtensilios
                .ProyectarALineaDeCategoria();

            if (!string.IsNullOrEmpty(criterios.BuscarPor))
            {
                consulta = consulta.Where(m => criterios.BuscarPor.Contains(m.Nombre) || m.Nombre.Contains(criterios.BuscarPor));
            }

            return await consulta
                        .OrderBy(m=>m.Nombre)
                        .ToPagedListAsync(indicePagina, tamañoPagina);

        }

        public async Task<List<ElementoValorDescripcion>> ElementosDeCategorias(string contiene = null, int maximoElementos = 10)
        {
            return await _db.CategoriasDeUtensilios
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
