using System.ComponentModel.DataAnnotations;
using Blog.Modelo.Imagenes;

namespace Blog.Smoothies.Views.Shared.ViewModels
{
    public class EditorImagen
    {
        public EditorImagen()
        {
            
        }
        public EditorImagen(Imagen imagen, string accionSubirImagen, string accionQuitarImagen)
        {
            AltImagen = imagen.Alt;
            UrlImagen = imagen.Url;

            AccionQuitarImagen = accionQuitarImagen;
            AccionSubirImagen = accionSubirImagen;
        }

        [Display(Name = "Alt imagen")]
        public string AltImagen { get; set; }

        [Display(Name = "Url imagen")]
        public string UrlImagen { get; set; }

        public string AccionQuitarImagen { get; set; }
        public string AccionSubirImagen { get; set; }

        public bool TieneImagen => !string.IsNullOrEmpty(UrlImagen);
    }
}