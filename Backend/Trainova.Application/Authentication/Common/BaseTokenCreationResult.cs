namespace Trainova.Application.Authentication.Common
{
    public abstract class BaseTokenCreationResult
    {
        protected BaseTokenCreationResult(string tokenType)
        {
            TokenType = tokenType;
        }

        public virtual string TokenType { get; init; }
    }
    public class RefreshTokenCreationResult : BaseTokenCreationResult
    {
        public string RefreshToken { get; init; }
        public RefreshTokenCreationResult(string refreshToken)
            : base("RefreshToken")
        {
            RefreshToken = refreshToken;
        }
    }
    public class ConfirmEmailTokenCreationResult : BaseTokenCreationResult
    {
        public string Email { get; init; }
        public ConfirmEmailTokenCreationResult(string email)
            : base("ConfirmEmail")
        {
            Email = email;
        }
    }
    public class PassWordResetTokenCreationResult : BaseTokenCreationResult
    {
        public Guid UserId { get; set; }
        public string Name { get; init; }
        public string ShowName { get; init; }
        public string? PhotoPath { get; init; }
        public PassWordResetTokenCreationResult(Guid userId, string name, string showName, string? photoPath = null)
            : base("PasswordReset")
        {
            Name = name;
            ShowName = showName;
            UserId = userId;
            PhotoPath = photoPath;
        }
    }
}
