using Trainova.Application.Common.Interfaces.Service;

namespace Trainova.Infrastructure.Authorization.AuthEmailSenders
{
    public class AuthEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
