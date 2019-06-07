using Blog.Modelo.Posts;
using Infra;

namespace LG.Web.Views.Blog.ViewModels
{
    public class PostUtensilioViewModel
    {
        public PostUtensilioViewModel()
        {
            
        }

        public PostUtensilioViewModel(PostUtensilio postUtensilio)
        {
            Nombre = postUtensilio.Utensilio.Nombre;
            Imagen = postUtensilio.Utensilio.Imagen;
            Link = postUtensilio.Utensilio.Link;
        }

        public string Nombre { get; private set; }

        public Imagen Imagen { get; set; }

        public string Link { get; private set; }
    }
}