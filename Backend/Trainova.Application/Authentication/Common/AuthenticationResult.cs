using Trainova.Domain.Users;

namespace Trainova.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);