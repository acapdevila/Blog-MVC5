using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels.Libros
{
    public  class LibroViewmodel
    {
        public string Url { get; set; }

        public string UrlImagen { get; set; }

        public string AltImagen { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }
    }
}
