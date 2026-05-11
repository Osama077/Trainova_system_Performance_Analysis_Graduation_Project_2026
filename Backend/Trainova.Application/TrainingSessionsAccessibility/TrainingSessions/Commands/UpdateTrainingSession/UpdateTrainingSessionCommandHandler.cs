using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession
{
    public class UpdateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateTrainingSessionCommand, ResultOf<TrainingSession>>
    {
        public async Task<ResultOf<TrainingSession>> Handle(UpdateTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                // Get existing training session
                var session = await _trainingSessionRepository.GetByIdAsync(request.Id);
                if (session == null)
                {
                    return Error.NotFound(
                        code: "UpdateTrainingSessionCommandHandler.Handle_SessionNotFound",
                        description: "Training session not found");
                }

                await _unitOfWork.StartTransactionAsync();

                // Update training session
                session.Update(request.SessionName, request.Place, request.PlanState, request.WillHappenAt, request.State);

                await _trainingSessionRepository.UpdateAsync(session);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return session.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "UpdateTrainingSessionCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
