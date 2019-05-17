using System.Collections.Generic;

namespace Blog.Modelo.Utensilios
{
    public class UtensilioCategoria
    {
        public UtensilioCategoria()
        {
            
        }
        public UtensilioCategoria(string nombre, string urlSlug, int posicion)
        {
            Nombre = nombre;
            UrlSlug = urlSlug;
            Posicion = posicion;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string UrlSlug { get; set; }

        public int Posicion { get; set; }

        public ICollection<Utensilio> Utensilios { get; set; }

    }
}
