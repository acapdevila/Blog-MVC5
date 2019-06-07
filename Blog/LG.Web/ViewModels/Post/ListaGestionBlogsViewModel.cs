using System.Collections.Generic;

namespace LG.Web.ViewModels.Post
{
    public class ListaGestionBlogsViewModel
    {
        public ListaGestionBlogsViewModel()
        {
            ListaBlogs = new List<LineaGestionBlog>();
        }

        public List<LineaGestionBlog> ListaBlogs { get; set; }
    }

    public class LineaGestionBlog
    {
        public LineaGestionBlog()
        {
           
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UrlSlug { get; set; }
        public string Autor { get; set; }
      
    }
}
