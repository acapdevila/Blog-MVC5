using System;
using System.Collections.Generic;
using Blog.Modelo.Posts;

namespace Blog.ViewModels.Blog
{
    public  class DetallesPostBlogViewModel
    {
        public Modelo.Posts.Post Post { get; set; }
        public List<LineaResumenPost> PostsSugeridos { get; set; }
    }
}
