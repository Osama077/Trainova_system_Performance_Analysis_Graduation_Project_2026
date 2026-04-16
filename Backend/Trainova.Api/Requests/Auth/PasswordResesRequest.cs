using MediatR;
using Trainova.Application.Authentication.Commands.PasswordReset;
using Trainova.Common.ResultOf;

namespace Trainova.Api.Requests.Auth
{
    public class PasswordResesRequest
    {
        public string NewPassword { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }

        public ResetPasswordCommand ToCommand()
        {
            return new ResetPasswordCommand(Email, Token, NewPassword);
        }
    }
}
