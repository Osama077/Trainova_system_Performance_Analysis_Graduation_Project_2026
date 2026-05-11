using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.UserAuth;

namespace Trainova.Infrastructure.Authorization.TokenGenerators
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly JwtSettings _jwtSettings;

        public TokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateJwtToken(User user)
        {
            if (_jwtSettings is null || user is null)
                throw new Exception("JwtSettings section not found in configuration!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString("N")),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.ShowName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }



    }
}
