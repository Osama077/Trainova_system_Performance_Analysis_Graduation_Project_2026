using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Common.Authorization;
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
    [Authorize(Roles = "Coach")]
    public record CreateFitnessSessionExerciseCommand(
        Guid SessionId,
        Guid ExerciseId,
        ExerciseIntensity Intensity,
        int? Sets = null,
        int? Reps = null,
        int? Rounds = null,
        int? ActiveTimeSec = null,
        int? RestTimeSec = null,
        string? LoadDetails = null) : IRequest<ResultOf<FitnessSessionExercise>>;

    public class CreateFitnessSessionExerciseCommandHandler : IRequestHandler<CreateFitnessSessionExerciseCommand, ResultOf<FitnessSessionExercise>>
    {
        private readonly IFitnessSessionExerciseRepository _sessionExerciseRepository;
        private readonly IFitnessExerciseRepository _exerciseRepository;
        private readonly ITrainingSessionRepository _trainingSessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public CreateFitnessSessionExerciseCommandHandler(
            IFitnessSessionExerciseRepository sessionExerciseRepository,
            IFitnessExerciseRepository exerciseRepository,
            ITrainingSessionRepository trainingSessionRepository,
            IUnitOfWork unitOfWork,
            CurrentUser currentUser)
        {
            _sessionExerciseRepository = sessionExerciseRepository;
            _exerciseRepository = exerciseRepository;
            _trainingSessionRepository = trainingSessionRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

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
                    request.SessionId,
                    exercise,
                    request.Intensity,
                    _currentUser.Id,
                    request.Sets,
                    request.Reps,
                    request.Rounds,
                    request.ActiveTimeSec,
                    request.RestTimeSec,
                    request.LoadDetails
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
