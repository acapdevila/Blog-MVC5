using Blog.Modelo.Utensilios;

namespace Blog.Modelo.Posts
{
    public class PostUtensilio
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public int UtensilioId { get; set; }

        public Post Post { get; set; }
        public Utensilio Utensilio { get; set; }
        public int Posicion { get; set; }
    }
}
