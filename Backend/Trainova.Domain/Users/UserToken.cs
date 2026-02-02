using Trainova.Domain.Common;

namespace Trainova.Domain.Users
{
    public class UserToken
    {
        public UserToken(Guid userId, string token, TokenType tokenType)
        {
            Id = Guid.NewGuid();
            Token = token;
            UserId = userId;
            TokenType = tokenType;
            IsUsed = false;
            UsedAt = null;
            IsRevoked = false;
            RevokedAt = null;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Token { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public TokenType TokenType { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? UsedAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }



        public bool IsExpired => CreatedAt.Add(TokenType.Expiration) < DateTime.UtcNow;


        public void MarkUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }
    }
}
