using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Blog.Modelo.Imagenes
{
    public class Imagen : ValueObject
    {
        public Imagen(string url, string alt)
        {
            Url = url;
            Alt = alt;
        }

        public string Url { get;  }
        public string Alt { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Alt;
        }

        public static implicit operator string(Imagen imagen)
        {
            return imagen.Url;
        }
    }
}
