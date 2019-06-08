using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Ac.Infra
{
    public class Imagen : ValueObject
    {
        public static Imagen Vacia = new Imagen(null, null); 

        protected Imagen()
        {

        }
        public Imagen(string url, string alt)
        {
            if(string.IsNullOrEmpty(url)) return;

            Url = url;
            Alt = alt;
        }

        public string Url { get; private set; }
        public string Alt { get; private set; }

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
