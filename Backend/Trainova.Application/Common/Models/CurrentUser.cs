namespace Trainova.Application.Common.Models;

using System.Security.Claims;

public record CurrentUser(
    Guid? Id,              // User Id
    Guid? ActorId,         // UserId or DeviceId or ServiceId
    CurrentUserType? UserType,
    string? Name,
    string? Email,
    UserIP? UserIP,
    string? Role,
    IReadOnlyList<Claim> Claims,
    bool? IsEmailConfirmed = null,
    bool? IsTFAEnabled = null
)
{
    public bool IsAuthenticated => ActorId.HasValue;

    public bool IsHuman =>
        UserType == CurrentUserType.User;

    public bool HasRelatedUser =>
        Id.HasValue;

    public bool IsInRole(string role) =>
        string.Equals(Role, role, StringComparison.OrdinalIgnoreCase);

    public IReadOnlyList<string> GetClaimValues(string claimType) =>
        Claims
            .Where(c => c.Type == claimType)
            .Select(c => c.Value)
            .ToList();

    public IReadOnlyList<Claim> GetClaims(string claimType) =>
        Claims
            .Where(c => c.Type == claimType)
            .ToList();

    public IReadOnlyList<Claim> GetClaims() =>
        Claims.ToList();

    public override string ToString()
    {
        var commonInfo =
            $"\nActor ID: {ActorId}" +
            $"\nFrom IP: {UserIP}" +
            $"\nRole: {Role}" +
            $"\nClaims: [{string.Join(", ", Claims.Select(c => $"{c.Type}: {c.Value}"))}]";

        return UserType switch
        {
            CurrentUserType.User =>
                $"[User] User ID: {Id}, Name: {Name}, Email: {Email}" +
                $"\nEmail Confirmed: {(IsEmailConfirmed == true ? "Active" : "Not Active")}" +
                $"\nTFA Status: {(IsTFAEnabled == true ? "Active" : "Not Active")}" +
                commonInfo,

            CurrentUserType.SmartWatch =>
                $"[SmartWatch] Device ID: {ActorId}, Owner ID: {Id}, Owner Name: {Name}, Owner Email: {Email}" +
                commonInfo,

            CurrentUserType.FitnessTracingDevice =>
                $"[FitnessDevice] Device ID: {ActorId}, Owner ID: {Id}, Owner Name: {Name}, Owner Email: {Email}" +
                commonInfo,

            CurrentUserType.MlModelService =>
                $"[ML Service] Service ID: {ActorId}, Service Name: {Name}" +
                commonInfo,

            _ =>
                $"[Unknown Type] Actor ID: {ActorId}, Name: {Name}" +
                commonInfo
        };
    }

    public static CurrentUser Anonymous(UserIP? userIP = null)
    {
        return new CurrentUser(
            Id: null,
            ActorId: null,
            UserType: null,
            Name: null,
            Email: null,
            UserIP: userIP ?? UserIP.FromString("0.0.0.0"),
            Role: null,
            Claims: Array.Empty<Claim>()
        );
    }
}
public record UserIP(byte[] AddressBytes)
{
    public override string ToString()
    {
        if (AddressBytes == null || AddressBytes.Length == 0)
            return "0.0.0.0";

        if (AddressBytes.Length == 4)
        {
            return $"{AddressBytes[0]}.{AddressBytes[1]}.{AddressBytes[2]}.{AddressBytes[3]}";
        }

        var segments = new string[8];
        for (int i = 0; i < 8; i++)
        {
            segments[i] = ((AddressBytes[i * 2] << 8) + AddressBytes[i * 2 + 1]).ToString("x");
        }
        return string.Join(":", segments);
    }

    public static UserIP FromString(string value)
    {
        if (System.Net.IPAddress.TryParse(value, out var ipAddress))
        {
            return new UserIP(ipAddress.GetAddressBytes());
        }
        return new UserIP(new byte[] { 127, 0, 0, 1 });
    }
}
public enum CurrentUserType
{
    User,
    SmartWatch,
    MlModelService,
    FitnessTracingDevice
}
