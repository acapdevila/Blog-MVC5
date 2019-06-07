using System.ComponentModel.DataAnnotations;
using Blog.Modelo.Utensilios;
using Blog.Servicios.Utensilios.Comandos;
using Blog.Smoothies.Views.Shared.ViewModels;
using Infra;

namespace Blog.Smoothies.Views.UtensiliosGestion.ViewModels.Editores
{
    public class EditorDeUtensilio
    {

        public EditorDeUtensilio()
        {
            EditorImagen = new EditorImagen();
        }
        public EditorDeUtensilio(string accionPost, string accionSubirImagen, string accionQuitarImagen)
        {
            AccionPost = accionPost;
            EditorImagen = new EditorImagen(Imagen.Vacia, accionSubirImagen, accionQuitarImagen);
        }

        public EditorDeUtensilio(Utensilio utensilio, string accionPost, string accionSubirImagen, string accionQuitarImagen)
        {
            Id = utensilio.Id;
            Nombre = utensilio.Nombre;
            Url = utensilio.Link;

            EditorImagen = new EditorImagen(utensilio.Imagen, accionSubirImagen, accionQuitarImagen);
            
            Categoria = utensilio.Categoria?.Nombre;
            AccionPost = accionPost;

        }

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Url")]
        public string Url { get; set; }

        public EditorImagen EditorImagen { get; set; }


        [Required]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        public string AccionPost { get; set; }
        
        public ComandoCrearUtensilio GenerarComandoCrearUtensilio()
        {
            return new ComandoCrearUtensilio
            {
                Id =  Id,
                Link = Url,
                Nombre = Nombre,
                ImagenAlt = EditorImagen.AltImagen,
                ImagenUrl = EditorImagen.UrlImagen,
                Categoria = Categoria
            };
        }

        public ComandoEditarUtensilio GenerarComandoEditarUtensilio()
        {
            return new ComandoEditarUtensilio
            {
                Id = Id,
                Link = Url,
                Nombre = Nombre,
                ImagenAlt = EditorImagen.AltImagen,
                ImagenUrl = EditorImagen.UrlImagen,
                Categoria = Categoria
            };
        }
    }
}