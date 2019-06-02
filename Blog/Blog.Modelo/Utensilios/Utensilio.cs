using System;
using Infra;

namespace Blog.Modelo.Utensilios
{
    public class Utensilio 
    {
        public Utensilio(string nombre, string link, Imagen imagen, UtensilioCategoria categoria)
        {
            CambiarNombre(nombre);
            CambiarLink(link);
            
            Imagen = imagen;
            Categoria = categoria;
        }

        private Utensilio()
        {
            
        }

        public int Id { get; set; }

        public string Nombre { get;private set; }

        public Imagen Imagen { get; set; }

        public string Link { get;private set; }

        public UtensilioCategoria Categoria { get; set; }


        public void CambiarNombre(string nombre)
        {
            if(string.IsNullOrEmpty(nombre))
                throw  new ArgumentNullException(nameof(nombre));

            Nombre = nombre;
        }

        public void CambiarLink(string link)
        {
            if (string.IsNullOrEmpty(link))
                throw new ArgumentNullException(nameof(link));

            Link = link;
        }

    }
}