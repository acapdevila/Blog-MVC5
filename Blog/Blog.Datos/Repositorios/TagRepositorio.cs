using System.Linq;
using Blog.Modelo;
using Blog.Modelo.Tags;

namespace Blog.Datos.Repositorios
{
    public class TagRepositorio : ITagRepositorio
    {
        private readonly ContextoBaseDatos _db;

        public TagRepositorio(ContextoBaseDatos contexto)
        {
            _db = contexto;
        }

        public Tag RecuperarTagPorNombre(string nombreTag)
        {
            return _db.Tags.FirstOrDefault(m => m.Nombre.ToLower() == nombreTag.ToLower());
        }
    }
}
