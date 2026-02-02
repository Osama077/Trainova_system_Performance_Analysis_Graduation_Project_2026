namespace Trainova.Application.Common.Interfaces.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

}
