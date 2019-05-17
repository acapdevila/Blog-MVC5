using System.Linq;
using Blog.Datos;

namespace Blog.Servicios.Utensilios
{
    public class GestorPosicionesCategoriasUtensilios
    {
        private readonly ContextoBaseDatos _db;

        public GestorPosicionesCategoriasUtensilios(ContextoBaseDatos db)
        {
            _db = db;
        }

        public int ObtenerPosicionParaNuevaCategoria()
        {
            return 100 + _db.CategoriasDeUtensilios.OrderByDescending(m => m.Posicion).FirstOrDefault()?.Posicion ?? 0;
        }
    }
}
