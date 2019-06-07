using System.Collections.Generic;

namespace Ac.Web.ViewModels.Libros
{
    public class LibrosProgramacionViewmodel
    {
        public List<CategoriaLibroViewmodel> Categorias { get; set; }
    }

    public class CategoriaLibroViewmodel
    {
        public string Nombre { get; set; }
        public List<LibroViewmodel> Libros { get; set; }
    }
}
