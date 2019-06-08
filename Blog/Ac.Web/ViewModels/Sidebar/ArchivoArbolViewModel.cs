using System;
using System.Collections.Generic;
using System.Linq;

namespace Ac.Web.ViewModels.Sidebar
{
    public class ArchivoArbolViewModel
    {
        public List<AñoArchivoArbolViewModel> Años { get; }
        
        public ArchivoArbolViewModel(List<ItemArchivoArbolViewModel> listaArchivo)
        {
            Años = new List<AñoArchivoArbolViewModel>();
            var años = listaArchivo.GroupBy(m => m.FechaPost.Year).Select(m=>m.Key).Distinct().ToList();
            foreach (var año in años)
            {
                Años.Add(new AñoArchivoArbolViewModel(año, listaArchivo));
            }
        }
    }

    public class AñoArchivoArbolViewModel
    {
        public AñoArchivoArbolViewModel(int año, List<ItemArchivoArbolViewModel> listaArchivo)
        {
            Año = año;
            Items = listaArchivo.Where(m => m.FechaPost.Year == año).ToList();
        }

        public int  Año { get; set; }

        public List<ItemArchivoArbolViewModel> Items { get; set; }
    }

    public class ItemArchivoArbolViewModel
    {
        public ItemArchivoArbolViewModel()
        {
            
        }
        public ItemArchivoArbolViewModel(Dominio.Posts.Post post)
        {
            FechaPost = post.FechaPost;
            Titulo = post.Titulo;
            Url = post.UrlSlug;

        }

        public DateTime FechaPost { get; set; }

        
        public string Titulo { get; set; }

        public string Url { get; set; }
    }
}
