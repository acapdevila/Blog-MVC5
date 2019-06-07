using System.Collections.Generic;
using Blog.Modelo.Posts;
using LG.Web.Views.Recetas.ViewModels;

namespace LG.Web.Views.Blog.ViewModels
{
    public  class DetallesPostBlogViewModel
    {
        public DisplayPostViewModel Post { get; set; }

        public DatosEstructuradosRecetaViewModel DatosEstructuradosReceta { get; set; }
        public List<LineaResumenPost> PostsSugeridos { get; set; }

    }
}
