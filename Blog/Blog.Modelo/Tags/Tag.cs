using System.Collections.Generic;

namespace Blog.Modelo.Tags
{
    public class Tag
    {
        public static readonly char Separador = ';'; 

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
