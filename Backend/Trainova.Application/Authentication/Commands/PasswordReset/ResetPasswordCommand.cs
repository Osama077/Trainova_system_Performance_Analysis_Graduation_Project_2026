using Trainova.Application.Authentication.Common;
using MediatR;
using Trainova.Common.ResultOf;


namespace Trainova.Application.Authentication.Commands.PasswordReset;

public partial record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword
) : IRequest<ResultOf<AuthenticationResult>>;


