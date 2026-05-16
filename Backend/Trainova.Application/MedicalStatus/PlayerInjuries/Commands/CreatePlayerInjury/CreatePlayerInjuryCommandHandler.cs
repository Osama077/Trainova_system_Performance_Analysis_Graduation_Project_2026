using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Domain.Profiles;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    public class CreatePlayerInjuryCommandHandler(
        IPlayerInjuryRepository playerInjuryRepository,
        IInjuryRepository _injuryRepository,
        IPlayerRepository _playerRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<CreatePlayerInjuryCommand, ResultOf<PlayerInjury>>
    {
        public async Task<ResultOf<PlayerInjury>> Handle(CreatePlayerInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(request.PlayerId);
                if (player == null)
                {
                    return Error.NotFound(
                        code: "CreatePlayerInjuryCommandHandler.Handle_PlayerNotFound",
                        description: $"Player with id {request.PlayerId} was not found.");
                }
                var injury = await _injuryRepository.GetByIdAsync(request.InjuryId);

                if (injury == null)
                {
                    return Error.NotFound(
                        code: "CreatePlayerInjuryCommandHandler.Handle_InjuryNotFound",
                        description: $"Injury with id {request.InjuryId} was not found.");
                }



                var playerInjury = new PlayerInjury(
                    player,
                    injury,
                    request.Status,
                    request.HappendAt,
                    request.Cause,
                    request.SevertiyGrade,
                    request.BodyPart,
                    request.Notes,
                    request.IsNew,
                    request.ExpectedReturnDate
                );
                await _unitOfWork.StartTransactionAsync();

                await playerInjuryRepository.AddAsync(playerInjury);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return playerInjury.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "CreatePlayerInjuryUnexpectedError", description: ex.Message);
            }
        }
    }
}
