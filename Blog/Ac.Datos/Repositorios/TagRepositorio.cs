using System.Linq;
using Ac.Dominio.Tags;

namespace Ac.Datos.Repositorios
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
