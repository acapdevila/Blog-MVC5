using System.Collections.Generic;

namespace Ac.Dominio.Tags
{
    public interface IEntidadConTags
    {
        ICollection<Tag> Tags { get; set; }
    }
}
