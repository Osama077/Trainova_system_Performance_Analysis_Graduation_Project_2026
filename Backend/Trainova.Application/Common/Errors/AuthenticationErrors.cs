using Trainova.Common.Errors;

namespace Trainova.Application.Common.Errors;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = Error.Validation(
        code: "Authentication.InvalidCredentials",
        description: "Invalid credentials");
}