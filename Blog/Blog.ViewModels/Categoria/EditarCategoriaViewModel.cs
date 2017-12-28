using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Blog.Modelo.Dtos;

namespace Blog.ViewModels.Categoria
{
    public  class EditarCategoriaViewModel
    {
        public EditarCategoriaViewModel(Modelo.Categorias.Categoria categoria)
        {
            Id = categoria.Id;
            BlogId = categoria.BlogId;
            Nombre = categoria.Nombre;
            UrlSlug = categoria.UrlSlug;
        }

        private string _urlSlug;

        public int Id { get; set; }
        public int BlogId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Escribe un nombre")]
        [StringLength(64, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Nombre { get; set; }


        [Display(Name = "Url del post")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string UrlSlug
        {
            get { return _urlSlug; }
            set { _urlSlug = string.IsNullOrEmpty(value) ? value : value.Replace(" ", "-"); }
        }
    }


    public static class EditarCategoriaViewModelExtensiones
    {
        public static CategoriaDto ToDto(this EditarCategoriaViewModel editor)
        {
            return new CategoriaDto
            {
                Nombre = editor.Nombre,
                UrlSlug = editor.UrlSlug,
                Id = editor.Id,
                BlogId = editor.BlogId
            };
            
        }
    }

}
