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
        private readonly string _tituloBlog;

        public CategoriasServicio(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        public IQueryable<Categoria> Categorias()
        {
            return _db.Categorias
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));
        }


        private IQueryable<Categoria> CategoriasIncluyendoPosts()
        {
            return _db.Categorias
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));
        }

        public Task<Categoria> RecuperarCategoriaIncluyendoPostsPorUrlCategoriaAsync(string urlCategoria)
        {
            return CategoriasIncluyendoPosts()
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
