using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise
{
    [Authorize(Roles = "Coach")]
    public record UpdateExerciseCommand(
            Guid Id,
            string? Name = null,
            ExerciseType? Type = null) : IRequest<ResultOf<FitnessExercise>>;

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

                exercise.Update(request.Name, request.Type);

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
