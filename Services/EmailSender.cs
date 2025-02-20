using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace centuras.org.Services
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromAddress = configuration["EmailSettings:DefaultEmailAddress"];
            string smtpServer = configuration["EmailSettings:Server"];
            int smtpPort = Convert.ToInt32(configuration["EmailSettings:Port"]);
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            await client.SendMailAsync(message);
        }
    }
}
