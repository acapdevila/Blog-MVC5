using System.Linq;
using Blog.Modelo.Categorias;

namespace Blog.Datos.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ContextoBaseDatos _db;

        public CategoriaRepositorio(ContextoBaseDatos contexto)
        {
            _db = contexto;
        }

        public Categoria RecuperarCategoriaPorNombre(string nombreCategoria)
        {
            return _db.Categorias.FirstOrDefault(m => m.Nombre.ToLower() == nombreCategoria.ToLower());
        }
    }
}
