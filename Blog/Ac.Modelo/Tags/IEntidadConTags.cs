using System.Collections.Generic;

namespace Ac.Modelo.Tags
{
    public interface IEntidadConTags
    {
        ICollection<Tag> Tags { get; set; }
    }
}
