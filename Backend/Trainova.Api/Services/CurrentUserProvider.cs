using System.Security.Claims;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;

namespace Trainova.Api.Services
{
    public class CurrentUserProvider(
        IHttpContextAccessor _httpContextAccessor)
        : ICurrentUserProvider
    {

        public CurrentUser GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            // =========================
            // Get User IP
            // =========================

            string? ipString = null;

            // 1) Try X-Forwarded-For (proxy / load balancer)
            var forwardedFor = httpContext?
                .Request
                .Headers["X-Forwarded-For"]
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                ipString = forwardedFor.Split(',')[0].Trim();
            }
            else
            {
                ipString = httpContext?
                    .Connection
                    .RemoteIpAddress?
                    .ToString();
            }

            UserIP? userIP = null;

            if (!string.IsNullOrWhiteSpace(ipString))
            {
                try
                {
                    userIP = UserIP.FromString(ipString);
                }
                catch
                {
                }
            }

            // =========================
            // Not Authenticated
            // =========================

            if (user?.Identity?.IsAuthenticated != true)
                return new CurrentUser(
                    Id: null,
                    FullName: null,
                    Email: null,
                    UserIP: userIP,
                    Roles: Array.Empty<string>(),
                    Claims: Array.Empty<Claim>()
                );

            // =========================
            // Get User Id
            // =========================

            Guid? id = null;

            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(idClaim) &&
                Guid.TryParse(idClaim, out var parsedId))
            {
                id = parsedId;
            }

            // =========================
            // =========================

            var fullName = user.FindFirst(ClaimTypes.Name)?.Value;

            var email = user.FindFirst(ClaimTypes.Email)?.Value;

            var roles = user
                .FindAll(ClaimTypes.Role)
                .Select(r => r.Value)
                .ToArray();

            var claims = user.Claims.ToArray();



            return new CurrentUser(
                Id: id,
                FullName: fullName,
                Email: email,
                UserIP: userIP,
                Roles: roles,
                Claims: claims
            );
        }


    }
}
