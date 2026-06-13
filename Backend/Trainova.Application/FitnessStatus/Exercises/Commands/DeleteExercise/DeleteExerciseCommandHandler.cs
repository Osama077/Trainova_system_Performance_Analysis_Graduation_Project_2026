using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.DeleteExercise
{
    public class DeleteExerciseCommandHandler(
        IFitnessExerciseRepository _exerciseRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteExerciseCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);
                if (exercise == null)
                    return Error.NotFound(description: "Exercise not found.");


                await _unitOfWork.StartTransactionAsync();

                await _exerciseRepository.DeleteAsync(exercise);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return new Done(id: exercise.Id).NoContent;
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "DeleteExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
