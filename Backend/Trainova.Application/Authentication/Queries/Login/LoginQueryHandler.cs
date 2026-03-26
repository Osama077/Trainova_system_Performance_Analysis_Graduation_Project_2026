using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Application.Authentication.Queries.Login;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Services;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;

namespace Trainova.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository)
        : IRequestHandler<LoginQuery, ResultOf<AuthenticationResult>>
{
    public async Task<ResultOf<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByEmailAsync(query.Email);

        return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
            ? AuthenticationErrors.InvalidCredentials
            : new AuthenticationResult(user, _tokenGenerator.GenerateJwtToken(user));
    }
}