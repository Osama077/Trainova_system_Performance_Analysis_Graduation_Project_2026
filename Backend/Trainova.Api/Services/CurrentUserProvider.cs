using System.Security.Claims;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;

namespace Trainova.Api.Services;

public class CurrentUserProvider(
    IHttpContextAccessor _httpContextAccessor)
    : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return CurrentUser.Anonymous();
        }

        var user = httpContext.User;
        var userIP = ExtractUserIP(httpContext);

        if (user is null || !user.Identity?.IsAuthenticated == true)
        {
            return CurrentUser.Anonymous(userIP);
        }

        var actorId = ReadGuidClaim(user, "actor_id");
        var userId = ReadGuidClaim(user, "user_id");

        if (actorId is null)
        {
            return CurrentUser.Anonymous(userIP);
        }

        var currentUserType = Enum.TryParse<CurrentUserType>(user.FindFirst("user_type")?.Value, true, out var parsedType)
            ? parsedType
            : (CurrentUserType?)null;

        if (userId is null && currentUserType == CurrentUserType.User)
        {
            userId = actorId;
        }

        var name = user.FindFirst(ClaimTypes.Name)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        var role = user.FindFirst(ClaimTypes.Role)?.Value;

        bool? isEmailConfirmed = bool.TryParse(user.FindFirst("isEmailConfirmed")?.Value, out var parsedEmailConfirmed)
            ? parsedEmailConfirmed
            : null;

        bool? isTFAEnabled = bool.TryParse(user.FindFirst("isTFAEnabled")?.Value, out var parsedTFA)
            ? parsedTFA
            : null;

        return new CurrentUser(
            Id: userId,
            ActorId: actorId.Value,
            UserType: currentUserType,
            Name: name,
            Email: email,
            UserIP: userIP,
            Role: role,
            Claims: user.Claims.ToList(),
            IsEmailConfirmed: isEmailConfirmed,
            IsTFAEnabled: isTFAEnabled
        );
    }

    private static UserIP? ExtractUserIP(HttpContext httpContext)
    {
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        string? ipString = !string.IsNullOrWhiteSpace(forwardedFor)
            ? forwardedFor.Split(',')[0].Trim()
            : httpContext.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrWhiteSpace(ipString))
        {
            return null;
        }

        try
        {
            return UserIP.FromString(ipString);
        }
        catch
        {
            return null;
        }
    }

    private static Guid? ReadGuidClaim(ClaimsPrincipal? principal, string claimType)
    {
        if (principal is null) return null;

        var value = principal.FindFirst(claimType)?.Value;
        return Guid.TryParse(value, out var id) ? id : null;
    }
}