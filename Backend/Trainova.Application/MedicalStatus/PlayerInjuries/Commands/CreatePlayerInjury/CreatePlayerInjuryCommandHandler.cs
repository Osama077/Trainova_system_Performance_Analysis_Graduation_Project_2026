using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    public class CreatePlayerInjuryCommandHandler(
        IPlayerInjuryRepository playerInjuryRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<CreatePlayerInjuryCommand, ResultOf<PlayerInjury>>
    {
        public async Task<ResultOf<PlayerInjury>> Handle(CreatePlayerInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerInjury = new PlayerInjury(
                    request.PlayerId,
                    request.InjuryId,
                    (InjuryStatus)request.Status,
                    request.HappendAt,
                    (InjuryCause)request.Cause,
                    (SevertiyGrade)request.SevertiyGrade,
                    (BodyPart)request.BodyPart,
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
