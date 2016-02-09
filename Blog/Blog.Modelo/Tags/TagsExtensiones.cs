using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Modelo.Tags
{
    public static class TagsExtensiones
    {
        public static IQueryable<Tag> ConPostsPublicados(this IQueryable<Tag> tags)
        {
            return tags.Where(m => m.Posts.Any(p=> !p.EsBorrador && p.FechaPublicacion <= DateTime.Now));
        }
    }
}
