// ...existing code...
using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury
{
    [Trainova.Application.Common.Authorization.Authorize(Role = "Doctor")]
    public class UpdatePlayerInjuryCommandHandler(
        IPlayerInjuryRepository playerInjuryRepository,
        IUnitOfWork unitOfWork,
        CurrentUser currentUser)
        : IRequestHandler<UpdatePlayerInjuryCommand, ResultOf<PlayerInjury>>
    {
        public async Task<ResultOf<PlayerInjury>> Handle(UpdatePlayerInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = (await playerInjuryRepository.GetAllAsync(playerInjuryId: request.Id)).FirstOrDefault();

                if (existing == null)
                {
                    return Error.NotFound(code: "UpdatePlayerInjury_NotFound", description: "Player injury not found");
                }

                existing.Update(
                    request.HappendAt,
                    request.Cause,
                    request.SevertiyGrade,
                    request.BodyPart,
                    request.Notes,
                    request.IsNew,
                    request.NewStatus,
                    request.ReturnedAt,
                    request.ExpectedReturnDate
                );

                await playerInjuryRepository.UpdateAsync(existing);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return existing.AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "UpdatePlayerInjuryUnexpectedError", description: ex.Message);
            }
        }
    }
}
