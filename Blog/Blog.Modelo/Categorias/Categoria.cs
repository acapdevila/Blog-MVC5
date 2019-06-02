using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Modelo.Dtos;
using Infra;
using Blog.Modelo.Posts;

namespace Blog.Modelo.Categorias
{
    public class Categoria
    {
        public Categoria()
        {
            Posts = new List<Post>();
        }
        
        public int Id { get; set; }

        public string Nombre { get;private set; }

        public string NombreSinAcentos { get; set; }
        public string UrlSlug { get; set; }

        public string ContenidoHtml { get; set; }

        public string Descripcion { get; set; }
        public string PalabrasClave { get; set; }
        public string UrlImagenPrincipal { get; set; }


        public DateTime? FechaPublicacion { get; set; }
        
        public ICollection<Post> Posts { get; set; }

        public bool EsMostrarDatosEstructurados
        {
            get { return FechaPublicacion.HasValue && !string.IsNullOrEmpty(Descripcion) && !string.IsNullOrEmpty(UrlImagenPrincipal); }
        }

        public void CambiarNombre(string nombre)
        {
            Nombre = nombre;
            NombreSinAcentos = Nombre.RemoveDiacritics();
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

        public void CopiarValores(CategoriaDto categoriaDto)
        {
            CambiarUrlSlug(categoriaDto.UrlSlug);
            CambiarNombre(categoriaDto.Nombre);
            CambiarDescripcion(categoriaDto.Descripcion);
            CambiarPalabrasClave(categoriaDto.PalabrasClave);
            CambiarUrlImagenPrincipal(categoriaDto.UrlImagenPrincipal);
            ContenidoHtml = categoriaDto.ContenidoHtml;
        }
    }

    public static class ExtensionesCategoria
    {
        public static readonly char SeparadorCategorias = ';';

        //public static IQueryable<Categoria> ConPostsPublicados(this IQueryable<Categoria> categorias)
        //{
        //    return categorias.Where(m => m.Posts.Any(p => !p.EsBorrador && p.FechaPublicacion <= DateTime.Now));
        //}

        public static string CategoriasSeparadasPorComma(this ICollection<Categoria> categorias)
        {
            return categorias.Any() ? string.Join(SeparadorCategorias + " ", categorias.Where(m => !string.IsNullOrEmpty(m.Nombre)).Select(m => m.Nombre)) : string.Empty;
        }
    }
}
