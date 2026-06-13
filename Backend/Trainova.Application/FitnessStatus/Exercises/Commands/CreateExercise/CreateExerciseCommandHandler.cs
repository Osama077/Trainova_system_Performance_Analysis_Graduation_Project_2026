using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandHandler(
        IFitnessExerciseRepository _exerciseRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateExerciseCommand, ResultOf<FitnessExercise>>
    {
        public async Task<ResultOf<FitnessExercise>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var exercise = new FitnessExercise(
                    name: request.Name,
                    type: request.Type,
                    category: request.Category,
                    equipmentRequired: request.EquipmentRequired,
                    targetMuscleGroup: request.TargetMuscleGroups,
                    defaultIntensity: request.DefaultIntensity,
                    defaultSets: request.DefaultSets,
                    defaultRepsOrDuration: request.DefaultRepsOrDuration,
                    defaultRestBetweenSetsSec: request.DefaultRestBetweenSetsSec,
                    typicalLoad: request.TypicalLoad,
                    recoveryTimeHours: request.RecoveryTimeHours,
                    description: request.Description,
                    contraindications: request.Contraindications,
                    createdBy: _currentUser.Id
                );

                await _unitOfWork.StartTransactionAsync();

                await _exerciseRepository.AddAsync(exercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return exercise.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "CreateExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}

