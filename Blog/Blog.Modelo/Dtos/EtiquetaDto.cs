using Blog.Modelo.Tags;

namespace Blog.Modelo.Dtos
{
    public class EtiquetaDto
    {

        public EtiquetaDto()
        {
            
        }
        public EtiquetaDto(Tag tag)
        {
            Id = tag.Id;
            Nombre = tag.Nombre;
            UrlSlug = tag.UrlSlug;
            Descripcion = tag.Descripcion;
            PalabrasClave = tag.PalabrasClave;
            UrlImagenPrincipal = tag.UrlImagenPrincipal;
            ContenidoHtml = tag.ContenidoHtml;
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
