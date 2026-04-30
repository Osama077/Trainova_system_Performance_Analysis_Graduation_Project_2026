using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateAccessPolicy
{
    public class CreateAccessPolicyCommandHandler(
        IAccsessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateAccessPolicyCommand, ResultOf<AccessPolicy>>
    {
        public async Task<ResultOf<AccessPolicy>> Handle(CreateAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "CreateAccessPolicyCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                if (string.IsNullOrWhiteSpace(request.PolicyName))
                    return Error.Validation(code: "CreateAccessPolicyCommandHandler.Handle_InvalidPolicyName", description: "Policy name is required and cannot be empty");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Create access policy
                var policy = new AccessPolicy(request.PolicyName, _currentUser.Id);

                await _accessPolicyRepository.AddAsync(policy);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return policy.AsCreated();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "CreateAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
