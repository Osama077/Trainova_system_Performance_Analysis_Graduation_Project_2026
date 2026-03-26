using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Common.ResultOf;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Services;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;

namespace Trainova.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository,
    IUnitOfWork _unitOfWork)
        : IRequestHandler<RegisterCommand, ResultOf<AuthenticationResult>>
{
    public async Task<ResultOf<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {


        throw new NotImplementedException();
        //if (await _usersRepository.ExistsByEmailAsync(command.Email))
        //{
        //    return Error.Conflict(description: "User already exists");
        //}

        //var hashPasswordResult = _passwordHasher.HashPassword(command.Password);

        //if (hashPasswordResult.IsFailure)
        //{
        //    return hashPasswordResult.Errors;
        //}

        //var user = new User(
        //    command.FullName,
        //    command.ShowName,
        //    command.Email,
        //    hashPasswordResult.Value);



        //    await _usersRepository.AddUserAsync(user);



        //var token = _tokenGenerator.GenerateJwtToken(user);

        //return new AuthenticationResult(user, token);
    }
}