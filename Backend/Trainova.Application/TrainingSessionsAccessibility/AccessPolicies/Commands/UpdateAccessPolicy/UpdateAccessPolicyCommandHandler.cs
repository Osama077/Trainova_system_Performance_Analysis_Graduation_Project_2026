using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.UpdateAccessPolicy
{
    public class UpdateAccessPolicyCommandHandler(
        IAccsessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<UpdateAccessPolicyCommand, ResultOf<AccessPolicy>>
    {
        public async Task<ResultOf<AccessPolicy>> Handle(UpdateAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "UpdateAccessPolicyCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing access policy
                var policy = await _accessPolicyRepository.GetByIdAsync(request.Id);
                if (policy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "UpdateAccessPolicyCommandHandler.Handle_PolicyNotFound",
                        description: "Access policy not found");
                }

                // Update access policy
                policy.Update(request.PolicyName);

                await _accessPolicyRepository.UpdateAsync(policy);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return policy.AsNoContent();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "UpdateAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
