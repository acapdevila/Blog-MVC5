using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;

namespace LG.Web.ViewModels.Sidebar
{
    public class NubeEtiquetasViewModel
    {
        private List<Tag> Etiquetas { get; }
        
        public NubeEtiquetasViewModel(List<Tag> etiquetas)
        {
            Etiquetas = etiquetas;
        }
        
        public List<Tag> EtiquetasTodas => Etiquetas;

        public List<Tag> EtiquetasImpares => Etiquetas.Where((item, index) => index % 2 != 0).ToList();
        public List<Tag> EtiquetasPares => Etiquetas.Where((item, index) => index % 2 == 0).ToList();
    }
}
