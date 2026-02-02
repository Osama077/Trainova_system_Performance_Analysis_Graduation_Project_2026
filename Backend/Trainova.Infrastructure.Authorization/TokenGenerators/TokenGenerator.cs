using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.Authorization.TokenGenerators
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly JwtSettings _jwtSettings;

        public TokenGenerator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateJwtToken(User user, List<Role>? roles = null)
        {
            if (_jwtSettings is null || user is null)
                throw new Exception("JwtSettings section not found in configuration!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString("N")),
                new Claim("email", user.Email),
                new Claim("showName", user.ShowName)
            };
            if (roles is not null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim("roles", role.Name));
                }
            }
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.TokenExpirationInDays),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public UserToken GenerateUserTokens(User user, TokenType tokenType)
        {
            string token;

            switch (tokenType.Value)
            {
                case 4: // RefreshToken
                    token = GenerateSecureRandomString(128);
                    break;

                case 1: // EmailConfirmation
                case 2: // PasswordReset
                    token = GenerateSecureRandomString(6);
                    break;

                case 3: // TwoFactorAuthentication
                    token = GenerateNumericCode(6);
                    break;

                default:
                    throw new ArgumentException("Unsupported token type");
            }


            return new UserToken(user.Id, token, tokenType);

        }



        public static string GenerateSecureRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }

        private string GenerateNumericCode(int length)
        {
            var rng = new Random();
            return rng.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length)).ToString();
        }

    }
}
