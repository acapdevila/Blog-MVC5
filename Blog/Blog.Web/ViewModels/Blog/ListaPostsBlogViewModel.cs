using System;
using System.Collections.Generic;
using Blog.Modelo.Posts;

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

 
}
