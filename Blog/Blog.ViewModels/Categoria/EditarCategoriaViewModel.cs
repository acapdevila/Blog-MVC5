using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categoria
{
    public  class EditarCategoriaViewModel
    {
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
}
