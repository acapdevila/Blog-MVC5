namespace Blog.Modelo.Posts
{
    public class PostRelacionado
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public int HijoId { get; set; }

        public Post Post { get; set; }
        public Post Hijo { get; set; }
        public int Posicion { get; set; }
    }
}
