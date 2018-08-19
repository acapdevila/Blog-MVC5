using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Tags;
using PagedList;

namespace Blog.ViewModels.Post
{
    public class ListaGestionPostsViewModel
    {
        public ListaGestionPostsViewModel()
        {
         
        }

        public string BuscarPor { get; set; }
        public IPagedList<LineaGestionPost> ListaPosts { get; set; }
    }

    public class LineaGestionPost
    {
        public LineaGestionPost()
        {
            ListaTags = new List<Tag>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        
        public bool EsRssAtom { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }
        public ICollection<Tag> ListaTags { get; set; }
        public ICollection<Modelo.Categorias.Categoria> ListaCategorias { get; set; }

        public string Tags {
            get { return string.Join(" ", ListaTags.Select(m=>m.Nombre)); }
        }

        public string Categorias
        {
            get { return string.Join(" ", ListaCategorias.Select(m => m.Nombre)); }
        }
    }
}
