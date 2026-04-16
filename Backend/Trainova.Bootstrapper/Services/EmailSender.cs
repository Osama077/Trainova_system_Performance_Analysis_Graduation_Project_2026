using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Bootstrapper.Helpers;

namespace Trainova.Bootstrapper.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettingsOptions)
        {
            _emailSettings = emailSettingsOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var massege = new MailMessage
            {
                From = new MailAddress(_emailSettings.From),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            massege.To.Add(new MailAddress(email));
            var client = new SmtpClient(_emailSettings.Host,_emailSettings.Port);
            client.Credentials = new NetworkCredential(_emailSettings.UserName,_emailSettings.Password);
            client.EnableSsl = _emailSettings.EnableSsl;


            await client.SendMailAsync(massege);
        }
    }
}
