using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;
using Blog.ViewModels.Post;

namespace Blog.Servicios.Blog.Borradores
{
    public class BuscadorBorradores
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;

        public BuscadorBorradores(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        public async Task<List<LineaBorrador>> ObtenerListaBorradoresAsync(CriteriosBusqueda criteriosBusqueda)
        {
            return await _db.Posts
                    .Where(m => m.Blog.Titulo == _tituloBlog)
                    .Borradores()
                    .BuscarPor(criteriosBusqueda)
                    .Select(m => new LineaBorrador
                    {
                        Id = m.Id,
                        UrlSlug = m.UrlSlug,
                        Titulo = m.Titulo,
                        FechaPost = m.FechaPost,
                        FechaPublicacion = m.EsBorrador ? (DateTime?)null : m.FechaPublicacion,
                        Autor = m.Autor,
                        ListaTags = m.Tags,
                        ListaCategorias = m.Categorias
                    })
                    .OrderByDescending(m => m.FechaPost)
                    .ToListAsync();
            ;
        }
    }
}
