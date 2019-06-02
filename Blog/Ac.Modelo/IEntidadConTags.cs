using System.Collections.Generic;

namespace Ac.Modelo
{
    public interface IEntidadConTags
    {
        ICollection<Tag> Tags { get; set; }
    }
}
