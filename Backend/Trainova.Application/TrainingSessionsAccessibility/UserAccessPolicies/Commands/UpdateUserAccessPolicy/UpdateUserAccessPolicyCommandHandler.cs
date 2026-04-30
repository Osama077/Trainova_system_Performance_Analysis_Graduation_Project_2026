using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.UpdateUserAccessPolicy
{
    public class UpdateUserAccessPolicyCommandHandler(
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<UpdateUserAccessPolicyCommand, ResultOf<UserAccessPolicy>>
    {
        public async Task<ResultOf<UserAccessPolicy>> Handle(UpdateUserAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "UpdateUserAccessPolicyCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                if (request.DoneScore.HasValue && (request.DoneScore < 0 || request.DoneScore > 100))
                    return Error.Validation(code: "UpdateUserAccessPolicyCommandHandler.Handle_InvalidScore", description: "Done score must be between 0 and 100");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing user access policy
                var userAccessPolicy = await _userAccessPolicyRepository.GetByIdAsync(request.Id);
                if (userAccessPolicy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "UpdateUserAccessPolicyCommandHandler.Handle_NotFound",
                        description: "User access policy not found");
                }

                // Update user access policy
                if (request.Status.HasValue || request.DoneScore.HasValue)
                {
                    var status = request.Status ?? userAccessPolicy.AttendanceState;
                    var score = request.DoneScore ?? userAccessPolicy.DoneScore;
                    userAccessPolicy.UpdateState(status, score);
                }

                await _userAccessPolicyRepository.UpdateAsync(userAccessPolicy);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return userAccessPolicy.AsNoContent();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "UpdateUserAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
