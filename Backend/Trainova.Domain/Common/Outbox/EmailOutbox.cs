namespace Trainova.Domain.Common.Outbox
{

    public class EmailOutbox
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;   // 👈
        public string UserEmail { get; set; } = string.Empty; // 👈

        public string EmailType { get; set; } = string.Empty;
        public string? Token { get; set; }
        public bool IsSent { get; set; }= false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public string? ErrorMessage { get; set; }
        public int RetryCount { get; set; }

        private EmailOutbox() { }
        public EmailOutbox(Guid userId, string userName, string userEmail, string emailType, string? token = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            EmailType = emailType;
            Token = token;
        }

        public static EmailOutbox MarkSent(PendingEmail pending)
        {
            return new EmailOutbox
            {
                Id = pending.Id,
                UserId = pending.UserId,
                UserName = pending.UserName,
                UserEmail = pending.UserEmail,
                EmailType = pending.EmailType,
                Token = pending.Token,
                CreatedAt = pending.CreatedAt,
                RetryCount = pending.RetryCount,
                IsSent = true,
                SentAt = DateTime.UtcNow,
                ErrorMessage = null
            };
        }

        public static EmailOutbox MarkFailed(PendingEmail pending, string errorMessage)
        {

            return new EmailOutbox
            {
                Id = pending.Id,
                UserId = pending.UserId,
                UserName = pending.UserName,
                UserEmail = pending.UserEmail,
                EmailType = pending.EmailType,
                Token = pending.Token,
                CreatedAt = pending.CreatedAt,
                RetryCount = pending.RetryCount + 1,
                IsSent = false,
                SentAt = null,
                ErrorMessage = errorMessage
            };
        }

    }



}
