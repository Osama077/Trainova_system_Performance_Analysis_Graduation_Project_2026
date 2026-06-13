using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise
{

    public class UpdateFitnessSessionExerciseCommandHandler(
        IFitnessSessionExerciseRepository _sessionExerciseRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateFitnessSessionExerciseCommand, ResultOf<FitnessSessionExercise>>
    {
        public async Task<ResultOf<FitnessSessionExercise>> Handle(UpdateFitnessSessionExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sessionExercise = await _sessionExerciseRepository.GetByIdAsync(request.Id);
                if (sessionExercise == null)
                    return Error.NotFound(description: "Exercise not found in this training session.");

                sessionExercise.Update(
                    intensity: request.Intensity,
                    sets: request.Sets,
                    repsOrDuration: request.RepsOrDuration,
                    restTimeSec: request.RestTimeSec,
                    loadDetails: request.LoadDetails,
                    rounds: request.Rounds,
                    activeTimeSec: request.ActiveTimeSec
                );

                await _unitOfWork.StartTransactionAsync();

                await _sessionExerciseRepository.UpdateAsync(sessionExercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return sessionExercise.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "UpdateFitnessSessionExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
