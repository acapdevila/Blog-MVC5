using Blog.Modelo.Categorias;

namespace Blog.Modelo.Dtos
{
    public class CategoriaDto
    {

        public CategoriaDto()
        {
            
        }
        public CategoriaDto(Categoria categoria)
        {
            Id = categoria.Id;
            Nombre = categoria.Nombre;
            UrlSlug = categoria.UrlSlug;
            Descripcion = categoria.Descripcion;
            PalabrasClave = categoria.PalabrasClave;
            UrlImagenPrincipal = categoria.UrlImagenPrincipal;
            ContenidoHtml = categoria.ContenidoHtml;
        }

        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string UrlSlug { get; set; }

        public string Descripcion { get; set; }

        public string PalabrasClave { get; set; }

        public string UrlImagenPrincipal { get; set; }

        public string ContenidoHtml { get; set; }

        
    }
}
