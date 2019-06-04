using System;
using System.Collections.Generic;
using Ac.Modelo;
using Ac.Modelo.Posts;

namespace Ac.ViewModels.Blog
{
    public  class DetallesPostBlogViewModel
    {
        public Modelo.Post Post { get; set; }


        public List<LineaResumenPost> PostsSugeridos { get; set; }
    }
}
