using MediatR;
using Microsoft.AspNetCore.Http;
using Trainova.Application.Authentication.Common;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Services;

namespace Trainova.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository,
    IHttpContextAccessor _contextAccessor)
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


            var token = _tokenGenerator.GenerateUserJwtToken(user);

            _contextAccessor.HttpContext.Response.Cookies.Append("access_token", token);

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