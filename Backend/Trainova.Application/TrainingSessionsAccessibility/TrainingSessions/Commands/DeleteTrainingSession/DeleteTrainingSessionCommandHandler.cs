using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.DeleteTrainingSession
{
    public class DeleteTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteTrainingSessionCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "DeleteTrainingSessionCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing training session
                var session = await _trainingSessionRepository.GetByIdAsync(request.Id);
                if (session == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "DeleteTrainingSessionCommandHandler.Handle_SessionNotFound",
                        description: "Training session not found");
                }

                // Delete training session
                await _trainingSessionRepository.DeleteAsync(session);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return Done.done.AsNoContent();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "DeleteTrainingSessionCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
