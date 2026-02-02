using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Domain.Common;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.ResultOf;
using Trainova.Application.Common.Interfaces.Services;

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