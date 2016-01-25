using System;
using System.Collections.Generic;
using System.Linq;
namespace Blog.Web.ViewModels.Post
{
    public class ListaGestionPostsViewModel
    {
        public ListaGestionPostsViewModel()
        {
            ListaPosts = new List<LineaGestionPost>();
        }

        public List<LineaGestionPost> ListaPosts { get; set; }
    }

    public class LineaGestionPost
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public DateTime FechaPost { get; set; }
        public bool EsBorrador { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }
    }
}
