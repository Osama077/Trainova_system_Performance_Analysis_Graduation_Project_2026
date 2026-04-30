using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession
{
    public class UpdateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<UpdateTrainingSessionCommand, ResultOf<TrainingSession>>
    {
        public async Task<ResultOf<TrainingSession>> Handle(UpdateTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "UpdateTrainingSessionCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing training session
                var session = await _trainingSessionRepository.GetByIdAsync(request.Id);
                if (session == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "UpdateTrainingSessionCommandHandler.Handle_SessionNotFound",
                        description: "Training session not found");
                }

                // Update training session
                session.Update(request.SessionName, request.Place, request.WillHappenAt, request.State);

                await _trainingSessionRepository.UpdateAsync(session);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return session.AsNoContent();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "UpdateTrainingSessionCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
