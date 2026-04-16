using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Bootstrapper.Helpers;
using Trainova.Domain.Common.Outbox;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Bootstrapper.BackgroundServises
{
    public class EmailOutboxBackGroundService : BackgroundService
    {
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(10);
        private readonly IServiceProvider _serviceProvider;

        public EmailOutboxBackGroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _emailOutboxRepository = scope.ServiceProvider.GetRequiredService<IEmailOutboxRepository>();
                    var _emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var pendingEmails = await _emailOutboxRepository.GetPendingAsync(50);
                    foreach (var email in pendingEmails)
                    {
                        try
                        {
                            var content = EmailBodyTemplates.GenerateTemplate(
                                email.UserName,
                                email.Token ?? "",
                                email.EmailType);

                            await _emailService.SendEmailAsync(email.UserEmail, content.Subject, content.Body);

                            await _emailOutboxRepository.UpdateAsync(EmailOutbox.MarkSent(email));
                        }
                        catch (Exception ex)
                        {
                            await _emailOutboxRepository.UpdateAsync(EmailOutbox.MarkFailed(email, ex.Message));
                        }
                        finally
                        {
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }

                }
                await Task.Delay(_delay);

            }
        }
    }
}
