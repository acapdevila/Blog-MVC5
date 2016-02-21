using System;
using System.Collections.Generic;
using Blog.Modelo.Posts;
using PagedList;

namespace Blog.Web.ViewModels.Blog
{
    public class ListaPostsBlogViewModel
    {
        public ListaPostsBlogViewModel()
        {
            
        }
        public IPagedList<LineaResumenPost> ListaPosts { get; set; }
    }

 
}
