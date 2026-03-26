using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.UserAuth.UserTokens
{
    public class UserToken
    {
        public Guid Id { get; private set; }
        public string Token { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public TokenType TokenType { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? UsedAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public RevokeCause? RevokeCause { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private UserToken() { }
        public UserToken(
            Guid userId,
            TokenType tokenType)
        {
            Id = Guid.NewGuid();
            Token = TokenType.Create();
            UserId = userId;
            TokenType = tokenType;
            IsUsed= false;
            UsedAt = null;
            IsRevoked= false;
            RevokeCause = null;
            RevokedAt = null;
            CreatedAt= DateTime.UtcNow;
        }
        public bool IsExpired => CreatedAt.Add(TokenType.Expiration) < DateTime.UtcNow;


        public void MarkUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }

        public void Revoke(RevokeCause cause = UserTokens.RevokeCause.NotDetermine)
        {
            RevokeCause = cause;
            IsUsed = false;
            UsedAt = null;
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }
}
