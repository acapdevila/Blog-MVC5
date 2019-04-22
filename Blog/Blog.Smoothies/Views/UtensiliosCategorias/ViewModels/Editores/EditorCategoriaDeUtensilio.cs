using System.ComponentModel.DataAnnotations;
using Blog.Modelo.Utensilios;
using Blog.Servicios.Utensilios.Comandos;

namespace Blog.Smoothies.Views.UtensiliosCategorias.ViewModels.Editores
{
    public class EditorCategoriaDeUtensilio 
    {
        public EditorCategoriaDeUtensilio()
        {
            
        }

        public EditorCategoriaDeUtensilio(UtensilioCategoria categoria)
        {
            Id = categoria.Id;
            Nombre = categoria.Nombre;
            Url = categoria.UrlSlug;

        }

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Url")]
        public string Url { get; set; }

        public ComandoCrearCategoriaUtensilio GenerarComandoCrearCategoriaUtensilio()
        {
            return new ComandoCrearCategoriaUtensilio
            {
                Id =  Id,
                UrlSlug = Url,
                Nombre = Nombre
            };
        }

        public ComandoEditarCategoriaUtensilio GenerarComandoEditarCategoriaUtensilio()
        {
            return new ComandoEditarCategoriaUtensilio
            {
                Id = Id,
                UrlSlug = Url,
                Nombre = Nombre
            };
        }
    }
}