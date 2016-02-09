using System.Collections.Generic;

namespace Blog.Modelo.Tags
{
    public interface IEntidadConTags
    {
        ICollection<Tag> Tags { get; set; }
    }
}
