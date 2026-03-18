using System.Security.Claims;

namespace Trainova.Application.Common.Models;

using System.Security.Claims;

public record CurrentUser(
    Guid? Id,
    string? FullName,
    string? Email,
    IReadOnlyList<string> Roles,
    IReadOnlyList<Claim> Claims
)
{
    public bool IsAuthenticated => Id.HasValue;

    public bool IsInRole(string role) =>
        Roles.Contains(role);

    public IReadOnlyList<string> GetClaimValues(string claimType) =>
        Claims.Where(c => c.Type == claimType)
              .Select(c => c.Value)
              .ToArray();

    public IReadOnlyList<Claim> GetClaims(string claimType) =>
        Claims.Where(c => c.Type == claimType)
              .ToArray();

    public IReadOnlyList<Claim> GetClaims() =>
        Claims.ToArray();
}

