using System;
using System.Collections.Generic;
using Blog.Modelo.Posts;
using Blog.Smoothies.Views.Recetas.ViewModels;

namespace Blog.Smoothies.Views.Blog.ViewModels
{
    public  class DetallesPostBlogViewModel
    {
        public DisplayPostViewModel Post { get; set; }

        public DatosEstructuradosRecetaViewModel DatosEstructuradosReceta { get; set; }
        public List<LineaResumenPost> PostsSugeridos { get; set; }

    }
}
