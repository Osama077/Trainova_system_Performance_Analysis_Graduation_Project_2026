using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Domain.TrainingSessionsAccessibility.Events;

namespace Trainova.Application.TrainingSessionsAccessibility.EventsHandlers
{
    public class SessionUserAccessPolicyCreatedEventHandler(
        IUsersRepository _usersRepository,
        IEmailSender _emailSender) : INotificationHandler<SessionUserAccessPolicyCreatedEvent>
    {

        public async Task Handle(SessionUserAccessPolicyCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(notification.UserId);
            if (user == null) return;

            var sessionTimeStr = notification.HappenedAt.HasValue
                ? notification.HappenedAt.Value.ToString("f")
                : "An unspecified time";

            var locationStr = !string.IsNullOrWhiteSpace(notification.Place)
                ? notification.Place
                : "Online / Not specified";

            var subject = $"New Training Session: {notification.SessionName}";

            var body = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.05);'>
                <div style='background-color: #4F46E5; padding: 24px; text-align: center; color: white;'>
                    <h2 style='margin: 0; font-size: 24px; font-weight: 600;'>New Session Scheduled 🎉</h2>
                </div>
                <div style='padding: 32px; color: #333333; line-height: 1.6;'>
                    <p style='font-size: 16px; margin-top: 0;'>Hello <strong>{user.ShowName}</strong>,</p>
                    <p style='font-size: 15px;'>You have been successfully added to a new training session. Here are the details:</p>
                    
                    <div style='background-color: #F9FAFB; border-left: 4px solid #4F46E5; padding: 16px; margin: 24px 0; border-radius: 0 4px 4px 0;'>
                        <p style='margin: 0 0 8px 0;'><strong>📚 Session:</strong> {notification.SessionName}</p>
                        <p style='margin: 0 0 8px 0;'><strong>📅 Date & Time:</strong> {sessionTimeStr}</p>
                        <p style='margin: 0;'><strong>📍 Location:</strong> {locationStr}</p>
                    </div>

                    <p style='font-size: 15px;'>Please check your dashboard to review your schedule and prepare for the session.</p>
                    
                    <hr style='border: 0; border-top: 1px solid #eeeeee; margin: 32px 0;' />
                    
                    <p style='font-size: 14px; color: #666666; margin-bottom: 0;'>
                        Best regards,<br>
                        <strong style='color: #4F46E5;'>Trainova Team</strong>
                    </p>
                </div>
            </div>";

            await _emailSender.SendEmailAsync(user.Email, subject, body);
        }
    }
}
