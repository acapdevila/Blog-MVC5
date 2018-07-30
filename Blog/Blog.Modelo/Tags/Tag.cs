using System;
using System.Collections.Generic;
using Blog.Modelo.Dtos;
using Blog.Modelo.Posts;

namespace Blog.Modelo.Tags
{
    public class Tag
    {
        public Tag()
        {
            Posts = new List<Post>();
        }

        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UrlSlug { get; set; }


        public string Descripcion { get; set; }
        public string PalabrasClave { get; set; }
        public string UrlImagenPrincipal { get; set; }
        public string ContenidoHtml { get; set; }


        public DateTime? FechaPublicacion { get; set; }

        public bool EsMostrarDatosEstructurados
        {
            get { return FechaPublicacion.HasValue && !string.IsNullOrEmpty(Descripcion) && !string.IsNullOrEmpty(UrlImagenPrincipal); }
        }

        public ICollection<Post> Posts { get; set; }



        public void CambiarNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void CambiarUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        public void CambiarDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }

        public void CambiarUrlImagenPrincipal(string urlImagenPrincipal)
        {
            UrlImagenPrincipal = urlImagenPrincipal;
        }
        public void CambiarPalabrasClave(string palabrasClave)
        {
            PalabrasClave = palabrasClave;
        }

        public void CopiarValores(EtiquetaDto etiquetaDto)
        {
            CambiarUrlSlug(etiquetaDto.UrlSlug);
            CambiarNombre(etiquetaDto.Nombre);
            CambiarDescripcion(etiquetaDto.Descripcion);
            CambiarPalabrasClave(etiquetaDto.PalabrasClave);
            CambiarUrlImagenPrincipal(etiquetaDto.UrlImagenPrincipal);
            ContenidoHtml = etiquetaDto.ContenidoHtml;
        }
    }
}
