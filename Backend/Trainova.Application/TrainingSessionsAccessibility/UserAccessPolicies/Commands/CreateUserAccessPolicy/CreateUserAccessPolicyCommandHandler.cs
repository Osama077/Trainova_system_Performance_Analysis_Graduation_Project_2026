using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.CreateUserAccessPolicy
{
    public class CreateUserAccessPolicyCommandHandler(
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IAccessPolicyRepository _accessPolicyRepository,
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateUserAccessPolicyCommand, ResultOf<UserAccessPolicy>>
    {
        public async Task<ResultOf<UserAccessPolicy>> Handle(CreateUserAccessPolicyCommand request, CancellationToken cancellationToken)
        {
            try
            {


                // Validate access policy exists
                var accessPolicy = await _accessPolicyRepository.GetByIdAsync(request.AccessPolicyId);
                if (accessPolicy == null)
                    return Error.NotFound(
                        code: "CreateUserAccessPolicyCommandHandler.Handle_PolicyNotFound",
                        description: "Access policy not found");

                // Validate user exists
                var user = await _usersRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return Error.NotFound(
                        code: "CreateUserAccessPolicyCommandHandler.Handle_UserNotFound",
                        description: "User not found");

                // Create user access policy
                var userAccessPolicy = new UserAccessPolicy(
                    request.AccessPolicyId,
                    request.UserId,
                    request.InitialStatus,
                    _currentUser.Id);

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                await _userAccessPolicyRepository.AddAsync(userAccessPolicy);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return userAccessPolicy.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "CreateUserAccessPolicyCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
