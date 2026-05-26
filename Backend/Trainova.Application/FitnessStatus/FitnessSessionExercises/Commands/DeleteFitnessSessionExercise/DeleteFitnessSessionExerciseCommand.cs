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

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.DeleteFitnessSessionExercise
{
    [Authorize(Roles = "Coach")]
    public record DeleteFitnessSessionExerciseCommand(Guid Id) : IRequest<ResultOf<Done>>;

    public class DeleteFitnessSessionExerciseCommandHandler : IRequestHandler<DeleteFitnessSessionExerciseCommand, ResultOf<Done>>
    {
        private readonly IFitnessSessionExerciseRepository _sessionExerciseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFitnessSessionExerciseCommandHandler(
            IFitnessSessionExerciseRepository sessionExerciseRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionExerciseRepository = sessionExerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<Done>> Handle(DeleteFitnessSessionExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fitnessSessionExercise = await _sessionExerciseRepository.GetByIdAsync(request.Id);
                if (fitnessSessionExercise == null)
                    return Error.NotFound(description: "Fitness session exercise mapping not found.");

                await _unitOfWork.StartTransactionAsync();
                await _sessionExerciseRepository.DeleteAsync(fitnessSessionExercise);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return new Done(id: fitnessSessionExercise.Id).NoContent;
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "DeleteFitnessSessionExerciseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
