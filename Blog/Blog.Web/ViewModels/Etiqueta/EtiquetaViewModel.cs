using System.Collections.Generic;
using Blog.Modelo.Posts;

namespace Blog.Web.ViewModels.Etiqueta
{
    public class EtiquetaViewModel
    {
        public string NombreEtiqueta { get; set; }
        public List<LineaResumenPost> ListaPosts { get; set; }
    }
}
