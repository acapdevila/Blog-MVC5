using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Modelo.Tags
{
    public static class ExtensionesTag
    {
        public static readonly char SeparadorTags = ';';

        public static IQueryable<Tag> ConPostsPublicados(this IQueryable<Tag> tags)
        {
            return tags.Where(m => m.Posts.Any(p=> !p.EsBorrador && p.FechaPublicacion <= DateTime.Now));
        }

        public static string TagsSeparadosPorComma(this ICollection<Tag> tags)
        {
            return tags.Any() ? string.Join(SeparadorTags + " ", tags.Select(m => m.Nombre)) : string.Empty;
        }
    }
}
