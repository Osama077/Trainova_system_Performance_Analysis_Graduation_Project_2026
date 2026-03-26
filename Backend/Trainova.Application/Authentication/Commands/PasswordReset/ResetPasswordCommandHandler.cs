using Trainova.Application.Authentication.Common;
using MediatR;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Domain.Common.Services;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;


namespace Trainova.Application.Authentication.Commands.PasswordReset
{
    public class ResetPasswordCommandHandler (
        IUsersRepository _usersRepository,
        ITokenGenerator _tokenGenerator,
        IUserTokensRepository _tokenRepsitory,
        IPasswordHasher _pawwwordHasher
        ) : IRequestHandler<ResetPasswordCommand, ResultOf<AuthenticationResult>>
    {
        public async Task<ResultOf<AuthenticationResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();


        }
    }
}
