using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;

namespace LG.Web.ViewModels.Post
{
    public class ListaBorradoresViewModel
    {
        public ListaBorradoresViewModel()
        {
         
        }

        public string BuscarPor { get; set; }
        public IList<LineaBorrador> ListaPosts { get; set; }
    }

    public class LineaBorrador
    {
        public LineaBorrador()
        {
            ListaTags = new List<Tag>();
            ListaCategorias = new List<global::Blog.Modelo.Categorias.Categoria>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Autor { get; set; }

        public ICollection<Tag> ListaTags { get; set; }
        public ICollection<global::Blog.Modelo.Categorias.Categoria> ListaCategorias { get; set; }

        public string Tags {
            get { return string.Join(" ", ListaTags.Select(m=>m.Nombre)); }
        }

        public string Categorias
        {
            get { return string.Join(" ", ListaCategorias.Select(m => m.Nombre)); }
        }
    }
}
