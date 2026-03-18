using Trainova.Common.SmartEnums;

namespace Trainova.Domain.UserAuth.UserTokens
{

    public class TokenType: SmartEnum<TokenType>
    {
        public TimeSpan Expiration { get; private set; }

        public static readonly TokenType EmailConfirmation = new TokenType("EmailConfirmation",1,TimeSpan.FromMinutes(10));
        public static readonly TokenType PasswordReset = new TokenType("PasswordReset", 2, TimeSpan.FromMinutes(10));
        public static readonly TokenType TwoFactorAuthentication = new TokenType("TwoFactorAuthentication", 3, TimeSpan.FromMinutes(10));
        public static readonly TokenType RefreshToken = new TokenType("RefreshToken", 4, TimeSpan.FromDays(5));
        private TokenType(string name,int value,TimeSpan expiration) : base(name, value)
        {
            Expiration = expiration;
        }


    }
}
