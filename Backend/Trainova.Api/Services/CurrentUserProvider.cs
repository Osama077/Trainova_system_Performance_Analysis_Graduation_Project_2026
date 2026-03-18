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
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                return new CurrentUser(
                    Id: null,
                    FullName: null,
                    Email: null,
                    Roles: Array.Empty<string>(),
                    Claims: Array.Empty<Claim>()
                );

            Guid? id = null;
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(idClaim) && Guid.TryParse(idClaim, out var parsedId))
            {
                id = parsedId;
            }

            var fullName = user.FindFirst(ClaimTypes.Name)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToArray();
            var claims = user.Claims.ToArray();

            return new CurrentUser(
                Id: id,
                FullName: fullName,
                Email: email,
                Roles: roles,
                Claims: claims
            );
        }
    }
}
