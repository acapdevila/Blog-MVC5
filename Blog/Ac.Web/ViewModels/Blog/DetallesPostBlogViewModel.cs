using System.Collections.Generic;
using Ac.Dominio.Posts;

namespace Ac.Web.ViewModels.Blog
{
    public  class DetallesPostBlogViewModel
    {
        public Dominio.Posts.Post Post { get; set; }


        public List<LineaResumenPost> PostsSugeridos { get; set; }
    }
}
