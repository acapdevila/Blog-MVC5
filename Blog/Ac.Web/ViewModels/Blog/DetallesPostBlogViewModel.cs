using System.Collections.Generic;
using Ac.Modelo.Posts;

namespace Ac.Web.ViewModels.Blog
{
    public  class DetallesPostBlogViewModel
    {
        public Modelo.Post Post { get; set; }


        public List<LineaResumenPost> PostsSugeridos { get; set; }
    }
}
