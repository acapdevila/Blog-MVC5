using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Posts;

namespace Blog.Servicios.Blog.Borradores
{
    public class BuscadorBorrador
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;

        public BuscadorBorrador(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        private IQueryable<Post> Posts()
        {
            return _db.Posts
                .Include(m=>m.Receta)
                .Include(m => m.Tags)
                .Include(m => m.Categorias)
                .Include(m=>m.PostRelacionados.Select(p=>p.Hijo))
                .Include(m => m.Utensilios.Select(p => p.Utensilio))
                .Where(m => m.Blog.Titulo == _tituloBlog);
        }


        public async Task<Post> BuscarBorradorPorIdAsync(int id)
        {
            return await Posts()
                    .Borradores()
                    .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
