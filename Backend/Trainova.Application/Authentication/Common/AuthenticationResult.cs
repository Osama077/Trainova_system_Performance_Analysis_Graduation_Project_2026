using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);