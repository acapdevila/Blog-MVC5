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

        public List<Categoria> CategoriasConPostsPublicados()
        {
            return Categorias().ConPostsPublicados().OrderBy(m => m.Nombre).ToList();
        }
        

        public async Task<Categoria> RecuperarCategoriaPorId(int id)
        {
            return await Categorias()
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CrearCategoria(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria();
            categoria.CambiarNombre(categoria.Nombre);
            categoria.CambiarUrlSlug(categoria.UrlSlug);
            _db.Categorias.Add(categoria);
            await _db.SaveChangesAsync();
            categoriaDto.Id = categoriaDto.Id;
        }

        public async Task ActualizarCategoria(CategoriaDto categoriaDto)
        {
            var categoria = await RecuperarCategoriaPorId(categoriaDto.Id);
            categoria.CambiarNombre(categoriaDto.Nombre);
            categoria.CambiarUrlSlug(categoriaDto.UrlSlug);
            await _db.SaveChangesAsync();
        }

        public async Task EliminarCategoria(int id)
        {
            var categoria = await RecuperarCategoriaPorId(id);
            _db.Categorias.Remove(categoria);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
         
        }

    
    }
}
