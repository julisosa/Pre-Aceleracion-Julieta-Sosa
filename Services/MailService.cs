using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Pre_Aceleracion_Julieta_Sosa.Services
{   
    public class MailService : IMailService
    {
        public async Task SendMail(string email, string subject, string htmlContent)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("julisoad15@gmail.com", "juliisoad");
            var to = new EmailAddress(email);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
