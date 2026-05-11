using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Services;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;

namespace Trainova.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository)
        : IRequestHandler<LoginQuery, ResultOf<AuthenticationResultBase>>
{
    public async Task<ResultOf<AuthenticationResultBase>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _usersRepository.GetByEmailAsync(query.Email);

            if (user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher))
                return AuthenticationErrors.InvalidCredentials;

            if (user.IsTFAEnabled)
            {
                user.CreateTFAToken();
                return ((AuthenticationResultBase)
                    new TFANeededAuthenticationResult(user)).AsPartial();
            }



            var token = _tokenGenerator.GenerateJwtToken(user);

            return ((AuthenticationResultBase)
                new FullAuthenticationResult(user, token)).AsDone();
        }
        catch (Exception ex)
        {
            return Error.Failure(
                code: "LoginQueryHandler.Handle_Failure",
                description: ex.Message
            );
        }

    }
}