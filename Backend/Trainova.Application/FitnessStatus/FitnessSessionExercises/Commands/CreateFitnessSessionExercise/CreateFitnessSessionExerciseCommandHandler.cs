using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.CreateFitnessSessionExercise
{

    public class CreateFitnessSessionExerciseCommandHandler(
        IFitnessSessionExerciseRepository _sessionExerciseRepository,
        IFitnessExerciseRepository _exerciseRepository,
        ITrainingSessionRepository _trainingSessionRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateFitnessSessionExerciseCommand, ResultOf<FitnessSessionExercise>>
    {
        public async Task<ResultOf<FitnessSessionExercise>> Handle(CreateFitnessSessionExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _trainingSessionRepository.GetByIdAsync(request.SessionId);
                if (session == null)
                    return Error.NotFound(description: "Training session not found.");

                var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);
                if (exercise == null)
                    return Error.NotFound(description: "Fitness exercise not found.");

                var existing = await _sessionExerciseRepository.GetBySessionAndExerciseIdAsync(request.SessionId, request.ExerciseId);
                if (existing != null)
                    return Error.Conflict(description: "Exercise is already added to this training session.");

                var fitnessSessionExercise = new FitnessSessionExercise(
                    sessionId: request.SessionId,
                    exercise: exercise,
                    intensity: request.Intensity,
                    sets: request.Sets,
                    repsOrDuration: request.RepsOrDuration,
                    restTimeSec: request.RestTimeSec,
                    loadDetails: request.LoadDetails,
                    rounds: request.Rounds,
                    activeTimeSec: request.ActiveTimeSec,
                    createdBy: _currentUser.Id
                );

                await _unitOfWork.StartTransactionAsync();

                await _sessionExerciseRepository.AddAsync(fitnessSessionExercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return fitnessSessionExercise.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "CreateFitnessSessionExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }



}
