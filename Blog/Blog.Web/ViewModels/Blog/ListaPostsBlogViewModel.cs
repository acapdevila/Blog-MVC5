using System;
using System.Collections.Generic;

namespace Blog.Web.ViewModels.Blog
{
    public class ListaPostsBlogViewModel
    {
        public ListaPostsBlogViewModel()
        {
            ListaPosts = new List<LineaResumenPost>();
        }

        public List<LineaResumenPost> ListaPosts { get; set; }
    }

    public class LineaResumenPost
    {
        public int Id { get; set; }
        public string UrlSlug { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public DateTime FechaPost { get; set; }
        public string Autor { get; set; }
    }
}
