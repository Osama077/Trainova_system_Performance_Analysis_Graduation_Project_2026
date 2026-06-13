using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise
{
    public class UpdateExerciseCommandHandler(
        IFitnessExerciseRepository _exerciseRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateExerciseCommand, ResultOf<FitnessExercise>>
    {
        public async Task<ResultOf<FitnessExercise>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);
                if (exercise == null)
                    return Error.NotFound(description: "Exercise not found.");

                exercise.Update(
                    name: request.Name,
                    type: request.Type,
                    category: request.Category,
                    equipmentRequired: request.EquipmentRequired,
                    targetMuscleGroup: request.TargetMuscleGroups,
                    defaultIntensity: request.DefaultIntensity,
                    defaultSets: request.DefaultSets,
                    defaultRepsOrDuration: request.DefaultRepsOrDuration,
                    defaultRestBetweenSetsSec: request.DefaultRestBetweenSetsSec,
                    description: request.Description,
                    contraindications: request.Contraindications
                );

                await _unitOfWork.StartTransactionAsync();

                await _exerciseRepository.UpdateAsync(exercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return exercise.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "UpdateExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}