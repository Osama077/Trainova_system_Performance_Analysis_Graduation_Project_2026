using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Infrastructure.Authorization.TokenGenerators
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly JwtSettings _jwtSettings;

        public TokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
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
                expires: DateTime.UtcNow.AddDays(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }



    }
}
