using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Authentication.Common;

public abstract class AuthenticationResultBase
{
    public bool Is2FARequired { get; init; }
    public string Name { get; init; }
    public string ShowName { get; init; }
    public string? PhotoPath { get; init; }
    public bool IsActive { get; init; }
    public AuthenticationResultBase(User user)
    {
        Is2FARequired = user.IsTFAEnabled;
        Name = user.FullName;
        ShowName = user.ShowName;
        PhotoPath = user.PhotoPath;
        IsActive = user.IsActive;
    }

}
public class TFANeededAuthenticationResult : AuthenticationResultBase
{
    public string EmailPrefix { get; init; }
    public TFANeededAuthenticationResult(User user) : base(user)
    {
        EmailPrefix = user.Email[0..3];
    }

}
public class FullAuthenticationResult : AuthenticationResultBase
{
    public string Token { get; init; }
    public string Email { get; init; }
    public bool IsConfirmedEmail { get; init; }
    public FullAuthenticationResult(User user, string token) : base(user)
    {
        Token = token;
        Email = user.Email;
        IsConfirmedEmail = user.IsEmailConfirmed;
    }
}