using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
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

        public string GenerateUserJwtToken(User user)
        {
            if (_jwtSettings is null || user is null)
                throw new Exception("JwtSettings section not found in configuration!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new("actor_id", user.Id.ToString("N")),
                new("user_id", user.Id.ToString("N")),

                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.ShowName),
                new Claim(ClaimTypes.Role, user.Role.Name),

                new Claim("isTFAEnabled", user.IsTFAEnabled.ToString()),
                new Claim("isEmailConfirmed", user.IsEmailConfirmed.ToString()),

                new Claim("user_type", CurrentUserType.User.ToString())
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
        public string GenerateDeviceJwtToken(Device device)
        {
            if (_jwtSettings is null || device is null)
                throw new Exception("JwtSettings section not found in configuration!");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var appUserType = device.UserType switch
            {
                DeviceType.SmartWatch => CurrentUserType.SmartWatch,
                DeviceType.MlModelService => CurrentUserType.MlModelService,
                DeviceType.FitnessTrackingDevice => CurrentUserType.FitnessTracingDevice,
                _ => throw new ArgumentOutOfRangeException()
            };

            var claims = new List<Claim>
            {
                new("actor_id", device.Id.ToString("N")),


                new Claim(
                    ClaimTypes.Name,
                    device.ServiceName),

                new Claim(
                    ClaimTypes.Role,
                    device.DeviceRole.Name),

                new Claim(
                    "device_identifier",
                    device.DeviceIdentifier),

                new Claim(
                    "user_type",
                    appUserType.ToString())
            };

            if (device.RelatedToUserId.HasValue)
            {
                claims.Add(
                    new Claim(
                        "user_id",
                        device.RelatedToUserId.Value.ToString("N")));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(365),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
