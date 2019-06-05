using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Categorias;
using Blog.Modelo.Dtos;
using PagedList;
using PagedList.EntityFramework;


namespace Blog.Servicios
{
    public class CategoriasServicio
    {
        private readonly ContextoBaseDatos _db;

        public CategoriasServicio(ContextoBaseDatos db)
        {
            _db = db;
        }

        public IQueryable<Categoria> Categorias()
        {
            return _db.Categorias;
        }



        public Task<Categoria> RecuperarCategoriaIncluyendoPostsPorUrlCategoriaAsync(string urlCategoria)
        {
            return Categorias()
                    .ConPosts()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlCategoria);
        }

        public async Task<IPagedList<LineaCategoriaDto>> ObtenerListaGestionCategorias(int numeroPagina, int categoriasPorPagina)
        {

            var lineas = await Categorias()
                .Select(m => new LineaCategoriaDto
            {
                Id = m.Id,
                UrlSlug = m.UrlSlug,
                Nombre = m.Nombre,
                NumeroPost = m.Posts.Count,
             })
                .OrderBy(m => m.Nombre) 
                .ToPagedListAsync(numeroPagina, categoriasPorPagina);

            return lineas;
        }

        public Task<List<Categoria>> BuscarCategoriasAsync(IReadOnlyList<string> palabrasBuscadas)
        {
            return Categorias().Where(c => palabrasBuscadas.Any(p => p.Contains(c.NombreSinAcentos) || c.NombreSinAcentos.Contains(p))).ToListAsync();
        }

        public async Task<List<CategoriaDto>> ObtenerListaCategorias()
        {

            var lineas = await Categorias()
                .Select(m => new CategoriaDto
                {
                    Id = m.Id,
                    UrlSlug = m.UrlSlug,
                    Nombre = m.Nombre
                })
                .OrderBy(m => m.Nombre)
                .ToListAsync();

            return lineas;
        }

     
        public async Task<Categoria> RecuperarCategoriaPorId(int id)
        {
            return await Categorias()
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearCategoria(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria();
            categoria.CopiarValores(categoriaDto);
            _db.Categorias.Add(categoria);
            await _db.GuardarCambios();
            categoriaDto.Id = categoria.Id;
        }

        public async Task ActualizarCategoria(CategoriaDto categoriaDto)
        {
            var categoria = await RecuperarCategoriaPorId(categoriaDto.Id);
            categoria.CopiarValores(categoriaDto);
            await _db.GuardarCambios();
        }

     

        public async Task EliminarCategoria(int id)
        {
            var categoria = await RecuperarCategoriaPorId(id);
            _db.Categorias.Remove(categoria);
            await _db.GuardarCambios();
        }

        public void Dispose()
        {
            _db.Dispose();
         
        }
        
    
    }
}
