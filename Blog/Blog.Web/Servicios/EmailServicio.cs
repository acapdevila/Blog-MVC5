using System.Net.Mail;
using Blog.Web.Configuracion;
using Blog.Web.ViewModels.Escribeme;

namespace Blog.Web.Servicios
{
    public interface IEmailServicio
    {
        void EnviarFormularioContacto(FormularioContactoViewModel formulario);
    }

    public class EmailServicio : IEmailServicio
    {
        public void EnviarFormularioContacto(FormularioContactoViewModel formulario)
        {
            if (!formulario.EsCaptchaValido) return;
            
            var fromAddress = new MailAddress(WebConfigParametro.EmailBlog, formulario.Nombre);
            var toAddress = new MailAddress(WebConfigParametro.EmailContactoBlog);

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = string.IsNullOrEmpty(formulario.Asunto) ? "Contacto Blog acapdevila" : formulario.Asunto,
                Body = $"{formulario.Mensaje}\r\n\r\n\r\nNombre: {formulario.Nombre}\r\nE-mail: {formulario.Email}\r\nTeléfono:{formulario.Telefono}\r\n* Mensaje enviado desde el formulario de contacto del Blog",
                IsBodyHtml = false
            };
            message.ReplyToList.Add(formulario.Email);
            Send(message);
        }

        private static void Send(MailMessage message)
        {
            var client = new SmtpClient();
            client.Send(message);
        }
    }
}