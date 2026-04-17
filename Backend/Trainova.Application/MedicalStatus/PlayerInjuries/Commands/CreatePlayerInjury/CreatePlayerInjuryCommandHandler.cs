// ...existing code...
using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    [Trainova.Application.Common.Authorization.Authorize(Role = "Doctor")]
    public class CreatePlayerInjuryCommandHandler(
        IPlayerInjuryRepository playerInjuryRepository,
        IUnitOfWork unitOfWork,
        CurrentUser currentUser)
        : IRequestHandler<CreatePlayerInjuryCommand, ResultOf<PlayerInjury>>
    {
        public async Task<ResultOf<PlayerInjury>> Handle(CreatePlayerInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerInjury = new PlayerInjury(
                    request.InjuryId,
                    request.PlayerId,
                    request.Status,
                    request.HappendAt,
                    request.Cause,
                    request.SevertiyGrade,
                    request.BodyPart,
                    request.Notes,
                    request.IsNew,
                    currentUser.Id
                );

                await playerInjuryRepository.AddAsync(playerInjury);

                await unitOfWork.SaveChangesAsync(cancellationToken);

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
