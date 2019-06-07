using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Contacto
{
    public class FormularioContactoViewModel
    {
        [Display(Name = @"Nombre *")]
        [Required(ErrorMessage = @"Escribe tu nombre")]
        [StringLength(50, ErrorMessage = @"La longitud máxima es de {1} carácteres")]
        public string Nombre { get; set; }

        [Display(Name = @"Email *")]
        [Required(ErrorMessage = @"Escribe tu email para que pueda contestarte")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Display(Name = @"Teléfono")]
        [StringLength(16, ErrorMessage = @"La longitud máxima es de {1} carácteres")]
        public string Telefono { get; set; }
        
        [StringLength(128, ErrorMessage = @"La longitud máxima es de {1} carácteres")]
        public string Asunto { get; set; }

        [Display(Name = @"Mensaje *")]
        [Required(ErrorMessage = @"Escribe un mensaje")]
        public string Mensaje { get; set; }

        [Display(Name = @"Huevo, perro, abeja o viernes, ¿cuál es el día de la semana?")]
        [Required(ErrorMessage = @"Este campo es obligatorio")]
        public string CaptchaDiaSemana { get; set; }

        public bool EsCaptchaValido => CaptchaDiaSemana.ToLower().Trim() == "viernes";
    }
}
