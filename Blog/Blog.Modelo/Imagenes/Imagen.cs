using System.Collections.Generic;
using Blog.Modelo.Recetas;

namespace Blog.Modelo.Imagenes
{
    public class Imagen
    {
        public int  Id { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }

        public ICollection<Receta> Recetas { get; set; }

    }
}
