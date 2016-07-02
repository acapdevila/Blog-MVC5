using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Tags;

namespace Blog.Servicios
{
    public class TagsServicio
    {
        private readonly ContextoBaseDatos _db;
        private readonly string _tituloBlog;

        public TagsServicio(ContextoBaseDatos db, string tituloBlog)
        {
            _db = db;
            _tituloBlog = tituloBlog;
        }

        private IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Where(m => m.Posts.Any(p => p.Blog.Titulo == _tituloBlog));
        }

        public async Task<List<Tag>> ObtenerListaTagsViewModel()
        {
            return await Tags().ToListAsync();
        }

        public async Task<Tag> RecuperarTag(int id)
        {
            return await Tags().FirstAsync(m => m.Id == id);
        }

        public async Task CrearTag(Tag tag)
        {
            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
            }

        public async Task ActualizarTag(Tag tag)
        {
            _db.Entry(tag).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task EliminarTag(int id)
        {
            var tag = await RecuperarTag(id);
            _db.Tags.Remove(tag);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
