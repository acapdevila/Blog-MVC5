using Blog.Modelo.Posts;

namespace Blog.ViewModels.Post
{
    public class EditorPostUtensilio
    {
        public EditorPostUtensilio()
        {

        }

        public EditorPostUtensilio(PostUtensilio postUtensilio)
        {
            Posicion = postUtensilio.Posicion;
            Nombre = postUtensilio.Utensilio.Nombre;
        }
        public int Posicion { get; set; }
        public string Nombre { get; set; }

        public bool EstaMarcadoParaEliminar { get; set; }
    }
}