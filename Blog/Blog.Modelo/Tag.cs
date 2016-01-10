using System.Collections.Generic;

namespace Blog.Modelo
{
    public class Tag
    {
        public Tag()
        {
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UrlSlug { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
