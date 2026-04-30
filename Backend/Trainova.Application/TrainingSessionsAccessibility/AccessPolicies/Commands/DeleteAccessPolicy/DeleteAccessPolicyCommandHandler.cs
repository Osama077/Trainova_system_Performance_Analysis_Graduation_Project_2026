using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.DeleteAccessPolicy
{
    public class DeleteAccessPolicyCommandHandler(
        IAccsessPolicyRepository _accessPolicyRepository,
        IPlanRepository _planRepository,
        ITrainingSessionRepository _trainingSessionRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteAccessPolicyCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "DeleteAccessPolicyCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing access policy
                var policy = await _accessPolicyRepository.GetByIdAsync(request.Id);
                if (policy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "DeleteAccessPolicyCommandHandler.Handle_PolicyNotFound",
                        description: "Access policy not found");
                }

                // Check if policy has associated plans
                if (await _planRepository.ExistsAsync(accessPolicyId: request.Id))
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.Conflict(
                        code: "DeleteAccessPolicyCommandHandler.Handle_PlansConflict",
                        description: "Cannot delete policy because it has associated plans");
                }

                // Check if policy has associated training sessions
                if (await _trainingSessionRepository.ExistsAsync(accessPolicyId: request.Id))
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.Conflict(
                        code: "DeleteAccessPolicyCommandHandler.Handle_SessionsConflict",
                        description: "Cannot delete policy because it has associated training sessions");
                }

                // Delete associated user access policies
                await _userAccessPolicyRepository.DeleteByPolicyIdAsync(request.Id);

                // Delete access policy
                await _accessPolicyRepository.DeleteAsync(policy);

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
                return Error.Unexpected(code: "DeleteAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
