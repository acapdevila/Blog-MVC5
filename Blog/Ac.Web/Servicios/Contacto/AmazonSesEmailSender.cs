using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Ac.Web.Servicios.Contacto
{
    public class AmazonSesEmailSender
    {
        private readonly AWSCredentials _credentials;
        private const string DisplayNameAndEmail = "=?ISO-8859-1?Q?Albert_Capdevila <info@bahiacode.com>";
        private const string ReturnEmail = "albert.capdevila@bahiacode.com";

        public AmazonSesEmailSender(AWSCredentials credentials)
        {
            _credentials = credentials;
        }

        public async Task EnviarEmailAsync(string email, string subject, string htmlMessage)
        {
            await EnviarEmailAsync(new Destination(new List<string> {email}), subject, htmlMessage);
        }

        public async Task EnviarEmailsAsync(List<string> emails, string subject, string htmlMessage)
        {
            var ratioPorSegundoSes = 45;
            var loteEmailsList = new List<string>();
            var countEmails = emails.Count;

            while (0 < countEmails)
            {
                for (int i = 0; i < ratioPorSegundoSes - 1; i++)
                {
                    var email = emails.First();
                    loteEmailsList.Add(email);
                    emails.Remove(email);
                    countEmails -= 1;
                    if (countEmails == 0) break;
                }

                if (0 < loteEmailsList.Count)
                {
                    var destinoLote = new Destination
                    {
                        BccAddresses = loteEmailsList
                    };
                    await EnviarEmailAsync(destinoLote, subject, htmlMessage);
                    await Task.Delay(1000); // esperar 1 segundo
                }

                loteEmailsList.Clear();
            }

        }


        public async Task<SendEmailResponse> EnviarEmailDeContactoAsync(
            string nombreRemitente,
            string email,
            string asunto,
            string cuerpoMensaje)
        {

            using (var client = new AmazonSimpleEmailServiceClient(_credentials, RegionEndpoint.EUWest1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = $"{nombreRemitente} <web@bahiacode.com>",
                    ReturnPath = ReturnEmail,
                    // ConfigurationSetName = formulario.Nombre,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { "albert.capdevila@bahiacode.com" },
                    },
                    ReplyToAddresses = new List<string> { email },

                    Message = new Message
                    {
                        Subject = new Content(string.IsNullOrEmpty(asunto)
                            ? "Contacto Blog"
                            : asunto),
                        Body = new Body
                        {
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = cuerpoMensaje,
                            }
                        }
                    },
                    // If you are not using a configuration set, comment
                    // or remove the following line 
                    //ConfigurationSetName = configSet
                };

                var response = await client.SendEmailAsync(sendRequest);

                return response;
            }




        }


        private async Task EnviarEmailAsync(Destination destino, string subject, string htmlMessage)
        {
            using (var client = new AmazonSimpleEmailServiceClient(_credentials, RegionEndpoint.EUWest1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = DisplayNameAndEmail,
                    ReturnPath = ReturnEmail,
                    // ConfigurationSetName = formulario.Nombre,
                    Destination = destino,

                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = htmlMessage,
                            }
                        }
                    },
                    // If you are not using a configuration set, comment
                    // or remove the following line 
                    //ConfigurationSetName = configSet
                };

                var response = await client.SendEmailAsync(sendRequest);
            }
        }

    }
}