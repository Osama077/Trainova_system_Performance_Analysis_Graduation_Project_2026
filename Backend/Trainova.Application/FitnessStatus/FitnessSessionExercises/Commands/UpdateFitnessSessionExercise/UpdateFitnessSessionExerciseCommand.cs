using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise
{
    [Authorize(Roles = "Coach")]
    public record UpdateFitnessSessionExerciseCommand(
        Guid Id,
        ExerciseIntensity? Intensity = null,
        int? Sets = null,
        int? Reps = null,
        int? Rounds = null,
        int? ActiveTimeSec = null,
        int? RestTimeSec = null,
        string? LoadDetails = null) : IRequest<ResultOf<FitnessSessionExercise>>;

    public class UpdateFitnessSessionExerciseCommandHandler : IRequestHandler<UpdateFitnessSessionExerciseCommand, ResultOf<FitnessSessionExercise>>
    {
        private readonly IFitnessSessionExerciseRepository _sessionExerciseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFitnessSessionExerciseCommandHandler(
            IFitnessSessionExerciseRepository sessionExerciseRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionExerciseRepository = sessionExerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<FitnessSessionExercise>> Handle(UpdateFitnessSessionExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fitnessSessionExercise = await _sessionExerciseRepository.GetByIdAsync(request.Id);
                if (fitnessSessionExercise == null)
                    return Error.NotFound(description: "Fitness session exercise mapping not found.");

                fitnessSessionExercise.Update(
                    request.Intensity,
                    request.Sets,
                    request.Reps,
                    request.Rounds,
                    request.ActiveTimeSec,
                    request.RestTimeSec,
                    request.LoadDetails
                );

                await _unitOfWork.StartTransactionAsync();
                await _sessionExerciseRepository.UpdateAsync(fitnessSessionExercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return fitnessSessionExercise.AsDone();
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
