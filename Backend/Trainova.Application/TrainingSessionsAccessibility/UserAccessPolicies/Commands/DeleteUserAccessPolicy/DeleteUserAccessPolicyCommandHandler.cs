using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.DeleteUserAccessPolicy
{
    public class DeleteUserAccessPolicyCommandHandler(
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteUserAccessPolicyCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteUserAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "DeleteUserAccessPolicyCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing user access policy
                var userAccessPolicy = await _userAccessPolicyRepository.GetByIdAsync(request.Id);
                if (userAccessPolicy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "DeleteUserAccessPolicyCommandHandler.Handle_NotFound",
                        description: "User access policy not found");
                }

                // Delete user access policy
                await _userAccessPolicyRepository.DeleteAsync(userAccessPolicy);

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
                return Error.Unexpected(code: "DeleteUserAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
