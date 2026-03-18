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



    }



}
