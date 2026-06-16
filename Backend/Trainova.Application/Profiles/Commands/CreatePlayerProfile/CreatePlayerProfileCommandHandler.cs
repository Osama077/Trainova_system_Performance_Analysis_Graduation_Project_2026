using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Services;
using Trainova.Domain.Profiles;
using Trainova.Domain.UserAuth;

namespace Trainova.Application.Profiles.Commands.CreatePlayerProfile;

public class CreatePlayerProfileCommandHandler(
    IUsersRepository _usersRepository,
    IPlayerRepository _playerRepository,
    IPasswordHasher _passwordHasher,
    IUnitOfWork _unitOfWork,
    CurrentUser _currentUser)
    : IRequestHandler<CreatePlayerProfileCommand, ResultOf<CreatePlayerProfileResponse>>
{
    public async Task<ResultOf<CreatePlayerProfileResponse>> Handle(CreatePlayerProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {

            if (await _usersRepository.ExistsByEmailAsync(request.Email))
                return Error.Conflict("EmailExistsAlready", "cant use already used Email");


            var user = new User(request.ShowName, request.FullName, request.Email, request.PhotoPath);

            var passwordResult = user.SetNewPassword(request.Password, _passwordHasher);
            if (passwordResult.IsFailure)
            {
                return passwordResult.Errors;
            }

            var player = new Player(
                id: user.Id,
                playerNumber: request.PlayerNumber,
                tShirtName: request.TShirtName,
                medecalStatus: request.MedicalStatus,
                currentMainPosition: request.CurrentMainPosition,
                otherAvailablePositions: request.OtherAvailablePositions,
                performanceLevel: request.PerformanceLevel,
                dateOfEnrolment: request.DateOfEnrolment,
                createdBy: _currentUser.Id);

            await _unitOfWork.StartTransactionAsync();

            await _usersRepository.AddUserAsync(user);
            await _playerRepository.AddAsync(player);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();

            return new CreatePlayerProfileResponse(
                player.Id,
                user.ShowName,
                user.FullName,
                player.PlayerNumber,
                player.TShirtName,
                player.CurrentMainPosition
            );
        }
        catch (DomainException ex)
        {
            return Error.DomainFailure(code: ex.Code, description: ex.Message);
        }
        catch (Exception ex)
        {
            return Error.Unexpected(code: "CreatePlayerProfileCommandHandler.Handle_Unexpected", description: ex.Message);
        }
    }
}
