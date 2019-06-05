using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Datos;
using Blog.Modelo.Dtos;
using Infra;
using Blog.Modelo.Posts;
using Blog.Modelo.Tags;

namespace Blog.Servicios
{
    public class TagsServicio
    {
        private readonly ContextoBaseDatos _db;

        public TagsServicio(ContextoBaseDatos db)
        {
            _db = db;
        }

        public IQueryable<Tag> Tags()
        {
            return _db.Tags
                .Where(m => m.Posts.Any());
        }

        private IQueryable<Tag> TagsIncluyendoPosts()
        {
            return _db.Tags
                .Include(m => m.Posts)
                .Where(m => m.Posts.Any());
        }


        public async Task<List<Tag>> BuscarTags(IReadOnlyCollection<string> palabrasBuscadas)
        {
            if (!palabrasBuscadas.Any())
                return new List<Tag>();

            var palabrasSinAcentos = palabrasBuscadas.Select(m => m.RemoveDiacritics()).ToList();

            return await Tags()
                .Where(m => palabrasSinAcentos.Contains(m.NombreSinAcentos))
                .ToListAsync();

        }


        public async Task<List<Tag>> RecuperarListaTagsAsync()
        {
            return await Tags().OrderBy(m=>m.Nombre).ToListAsync();
        }

        public List<Tag> RecuperarListaTags()
        {
            return Tags().OrderBy(m => m.Nombre).ToList();
        }


        public async Task<Tag> RecuperarTag(int id)
        {
            return await Tags().FirstAsync(m => m.Id == id);
        }

        public async Task CrearTag(EtiquetaDto etiquetaDto)
        {
            var etiqueta = new Tag();
            etiqueta.CopiarValores(etiquetaDto);
            _db.Tags.Add(etiqueta);
            await _db.GuardarCambios();
            etiquetaDto.Id = etiqueta.Id;
         }

        public async Task ActualizarTag(EtiquetaDto etiquetaDto)
        {
            var tag = await RecuperarTag(etiquetaDto.Id);
            tag.CopiarValores(etiquetaDto);
            await _db.GuardarCambios();
         }

        public async Task EliminarTag(int id)
        {
            var tag = await RecuperarTag(id);
            _db.Tags.Remove(tag);
            await _db.GuardarCambios();
        }

        public void Dispose()
        {
            _db.Dispose();
        }


        public async Task<Tag> RecuperarTagIncluyendoPostsPorUrlAsync(string urlSlug)
        {
           return  await TagsIncluyendoPosts()
                //  .ConPostsPublicados()
                .FirstOrDefaultAsync(m => m.UrlSlug == urlSlug);
        }
    }
}
