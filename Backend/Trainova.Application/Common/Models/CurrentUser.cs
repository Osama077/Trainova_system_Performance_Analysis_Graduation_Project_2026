using System.Security.Claims;

namespace Trainova.Application.Common.Models;

using System.Security.Claims;

public record CurrentUser(
    Guid? Id,
    string? FullName,
    string? Email,
    UserIP? UserIP,
    IReadOnlyList<string> Roles,
    IReadOnlyList<Claim> Claims
)
{
    public override string ToString()
    {
        return $"ID: {Id}, Name: {FullName}, Email: {Email}, Roles: [{string.Join(", ", Roles)}]";
    }
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
public record UserIP(byte O1,byte O2,byte O3,byte O4)
{
    public override string ToString()
    {
        return $"{O1}.{O2}.{O3}.{O4}";
    }
    public static UserIP FromString(string value)
    {
        var bytes = value.Split('.',
            options: StringSplitOptions.RemoveEmptyEntries);

        return new UserIP(
            Byte.Parse(bytes[0]),
            Byte.Parse(bytes[1]),
            Byte.Parse(bytes[2]),
            Byte.Parse(bytes[3])
        );
    }

}
