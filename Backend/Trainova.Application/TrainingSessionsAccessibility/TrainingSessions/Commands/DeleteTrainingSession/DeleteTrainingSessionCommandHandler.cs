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
        IUnitOfWork _unitOfWork,
        IAccessPolicyRepository _accessPolicyRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IPlanRepository _planRepository)
        : IRequestHandler<DeleteTrainingSessionCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return Error.Validation("DeleteTrainingSessionCommandHandler.Handle_NullRequest", "Request cannot be null");

                var session = await _trainingSessionRepository.GetByIdAsync(request.Id);

                if (session is null)
                    return Error.NotFound("DeleteTrainingSessionCommandHandler.Handle_SessionNotFound", "Training session not found");

                if (session.HappenedAt <= DateTime.UtcNow)
                    return Error.Conflict("DeleteTrainingSessionCommandHandler.Handle_ConflictDeleteStartedSession", " the Session should be already started");


                await _unitOfWork.StartTransactionAsync();

                var accessPolicyId = session.AccessPolicyId;

                await _trainingSessionRepository.DeleteAsync(session);

                var sessionsCount = await _trainingSessionRepository.CountByAccessPolicyIdAsync(accessPolicyId);
                var plansCount = await _planRepository.CountByAccessPolicyIdAsync(accessPolicyId);

                if (sessionsCount + plansCount ==1)
                {
                    await _userAccessPolicyRepository.DeleteByPolicyIdAsync(accessPolicyId);

                    var policy = await _accessPolicyRepository.GetByIdAsync(accessPolicyId);
                    if (policy is not null)
                        await _accessPolicyRepository.DeleteAsync(policy);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return Done.done.AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("DeleteTrainingSessionCommandHandler.Handle_Unexpected", ex.Message);
            }
        }

    }
}
