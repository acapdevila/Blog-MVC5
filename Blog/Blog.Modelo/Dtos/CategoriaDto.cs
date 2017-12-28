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
        }

        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string UrlSlug { get; set; }
        
    }
}
