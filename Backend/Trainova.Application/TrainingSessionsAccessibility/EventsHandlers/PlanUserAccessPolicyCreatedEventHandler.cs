using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Domain.TrainingSessionsAccessibility.Events;

namespace Trainova.Application.TrainingSessionsAccessibility.EventsHandlers
{
    public class PlanUserAccessPolicyCreatedEventHandler(
        IUsersRepository _usersRepository,
        IEmailSender _emailSender) : INotificationHandler<PlanUserAccessPolicyCreatedEvent>
    {
        public async Task Handle(PlanUserAccessPolicyCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(notification.UserId);
            if (user == null) return;

            var startDateStr = notification.StartDate.HasValue
                ? notification.StartDate.Value.ToString("D") // فورمت تاريخ طويل مناسب للخطط الكبيرة
                : "An unspecified date";

            var subject = $"Welcome to Your New Training Plan: {notification.PlanName}";

            var body = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.05);'>
                <div style='background-color: #10B981; padding: 24px; text-align: center; color: white;'>
                    <h2 style='margin: 0; font-size: 24px; font-weight: 600;'>Training Plan Activated 🚀</h2>
                </div>
                <div style='padding: 32px; color: #333333; line-height: 1.6;'>
                    <p style='font-size: 16px; margin-top: 0;'>Hello <strong>{user.ShowName}</strong>,</p>
                    <p style='font-size: 15px;'>Great news! You have been granted access to a new training plan designed to help you reach your goals.</p>
                    
                    <div style='background-color: #F9FAFB; border-left: 4px solid #10B981; padding: 16px; margin: 24px 0; border-radius: 0 4px 4px 0;'>
                        <p style='margin: 0 0 8px 0;'><strong>📋 Plan Name:</strong> {notification.PlanName}</p>
                        <p style='margin: 0;'><strong>🏁 Starts On:</strong> {startDateStr}</p>
                    </div>

                    <p style='font-size: 15px;'>Head over to your Trainova account to view the roadmap, milestones, and assigned content for this plan.</p>
                    
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 32px 0;' />
                    
                    <p style='font-size: 14px; color: #666666; margin-bottom: 0;'>
                        Best regards,<br>
                        <strong style='color: #10B981;'>Trainova Team</strong>
                    </p>
                </div>
            </div>";

            await _emailSender.SendEmailAsync(user.Email, subject, body);
        }
    }
}
