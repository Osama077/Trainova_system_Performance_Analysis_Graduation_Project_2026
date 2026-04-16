using Trainova.Domain.UserAuth.UserTokens;
namespace Trainova.Bootstrapper.Helpers
{
    public static class EmailBodyTemplates
    {
        public static EmailContent GenerateTemplate(string name, string token, string type)
        {
            if (TokenType.TryFromName(type.Trim(), out var tokenType,caseSensitive: false))
                return tokenType switch
                {
                    var t when t == TokenType.EmailConfirmation =>
                    new EmailContent(
                        "Trainova - Confirm Your Email",
                        EmailConfirmationTemplate(name, token)
                    ),

                    var t when t == TokenType.PasswordReset =>
                    new EmailContent(
                        "Trainova - Password Reset Request",
                        PasswordResetTemplate(name, token)
                    ),
                    var t when t == TokenType.TwoFactorAuthentication =>
                    new EmailContent(
                        "Trainova - Your Security Code",
                        TwoFactorTemplate(name, token)
                    ),
                    _ => throw new ArgumentException("Unsupported token type", nameof(type))
                };
            else
                throw new NotImplementedException(message: "Currently Just Auth Emails Supported");
        }

        private static string GetHeader() => @"
            <div style=""font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; max-width:600px; margin:auto; border:1px solid #e0e0e0; border-radius:12px; overflow:hidden; box-shadow: 0 4px 10px rgba(0,0,0,0.05);"">
                <div style=""background: linear-gradient(135deg, #1d3557 0%, #457b9d 100%); padding: 30px; text-align: center;"">
                    <h1 style=""color: #ffffff; margin: 0; font-size: 28px; letter-spacing: 1px;"">TRAINOVA</h1>
                    <p style=""color: #a8dadc; margin: 5px 0 0 0; font-size: 14px; text-transform: uppercase;"">Elevate Your Performance</p>
                </div>
                <div style=""padding: 30px; background-color: #ffffff;"">";

        private static string GetFooter() => @"
                </div>
                <div style=""background-color: #f8f9fa; padding: 20px; text-align: center; border-top: 1px solid #eeeeee;"">
                    <p style=""font-size: 12px; color: #777; margin: 0;"">© 2026 Trainova Fitness Platform. All rights reserved.</p>
                    <p style=""font-size: 12px; color: #777; margin: 5px 0 0 0;"">Stay active, stay strong.</p>
                </div>
            </div>";

        private static string GetTokenBox(string token) => $@"
            <div style=""margin: 30px auto; text-align: center;"">
                <div style=""display: inline-block; letter-spacing: 5px; font-family: monospace; font-size: 32px; font-weight: bold; padding: 15px 30px; background: #f1faee; color: #1d3557; border: 2px solid #a8dadc; border-radius: 8px;"">
                    {token}
                </div>
            </div>";

        private static string EmailConfirmationTemplate(string name, string token)
        {
            return $@"{GetHeader()}
                <h2 style=""color: #1d3557; margin-top: 0;"">Welcome to the Team, {name}! 👋</h2>
                <p style=""color: #444; line-height: 1.6; font-size: 16px;"">
                    We're excited to have you at <strong>Trainova</strong>. You're one step away from starting your fitness journey.
                </p>
                <p style=""color: #444; font-size: 15px;"">Please use the verification code below to confirm your email:</p>
                {GetTokenBox(token)}
                <p style=""color: #666; font-size: 13px; font-style: italic;"">
                    The hustle starts now. If you didn't sign up for Trainova, please ignore this email.
                </p>
            {GetFooter()}";
        }

        private static string PasswordResetTemplate(string name, string token)
        {
            return $@"{GetHeader()}
                <h2 style=""color: #1d3557; margin-top: 0;"">Reset Your Password 🔐</h2>
                <p style=""color: #444; line-height: 1.6; font-size: 16px;"">
                    Hello {name}, we received a request to reset your Trainova account password.
                </p>
                <p style=""color: #444; font-size: 15px;"">Use this secure code to proceed:</p>
                {GetTokenBox(token)}
                <p style=""color: #e63946; font-size: 13px; font-weight: bold;"">
                    If you didn't request this, please secure your account immediately.
                </p>
            {GetFooter()}";
        }

        private static string TwoFactorTemplate(string name, string token)
        {
            return $@"{GetHeader()}
                <h2 style=""color: #1d3557; margin-top: 0;"">Identity Verification 🛡️</h2>
                <p style=""color: #444; line-height: 1.6; font-size: 16px;"">
                    Hello {name}, someone is trying to access your Trainova account.
                </p>
                <p style=""color: #444; font-size: 15px;"">Your 2FA security code is:</p>
                {GetTokenBox(token)}
                <p style=""color: #666; font-size: 13px;"">
                    This code will expire shortly. Never share your code with anyone.
                </p>
            {GetFooter()}";
        }
    }
}