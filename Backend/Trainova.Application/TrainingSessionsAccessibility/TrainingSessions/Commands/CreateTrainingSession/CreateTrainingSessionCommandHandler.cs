using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public class CreateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IPlanRepository _planRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IAccsessPolicyRepository _accsessPolicyRepository,
        IUnitOfWork _unitOfWork,
        IUsersRepository _usersRepository,
        CurrentUser _currentUser)
        : IRequestHandler<CreateTrainingSessionCommand, ResultOf<TrainingSession>>
    {

        public async Task<ResultOf<TrainingSession>> Handle(CreateTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "CreateTrainingSessionCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                if (string.IsNullOrWhiteSpace(request.SessionName))
                {
                    return Error.Validation(code: "CreateTrainingSessionCommandHandler.Handle_InvalidSessionName", description: "Session name is required and cannot be empty");
                }

                // Validate UserIds when creating new policy
                if (!request.PolicyId.HasValue && (request.UserIds == null || request.UserIds.Count == 0))
                {
                    return Error.Validation(code: "CreateTrainingSessionCommandHandler.Handle_EmptyUserIds", description: "At least one user must be provided when creating a new access policy");
                }

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Handle access policy
                AccessPolicy? accessPolicy = null;
                if (request.PolicyId.HasValue)
                {
                    accessPolicy = await _accsessPolicyRepository.GetByIdAsync(request.PolicyId.Value);
                    if (accessPolicy == null)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Error.NotFound(
                            code: "CreateTrainingSessionCommandHandler.Handle_PolicyNotFound",
                            description: "Access policy not found");
                    }
                }
                else
                {
                    accessPolicy = new AccessPolicy(request.SessionName);
                    await _accsessPolicyRepository.AddAsync(accessPolicy);

                    // Add users to policy if provided
                    if (request.UserIds != null && request.UserIds.Any())
                    {
                        var userAccessPolicies = new List<UserAccessPolicy>();
                        foreach (var userId in request.UserIds)
                        {
                            var user = await _usersRepository.GetByIdAsync(userId);
                            if (user == null)
                            {
                                await _unitOfWork.RollbackTransactionAsync();
                                return Error.NotFound(
                                    code: "CreateTrainingSessionCommandHandler.Handle_UserNotFound",
                                    description: $"User with id: {userId} not found");
                            }

                            var userAccessPolicy = new UserAccessPolicy(accessPolicy.Id, userId, AttendanceStatus.Waiting);
                            userAccessPolicies.Add(userAccessPolicy);
                        }
                        await _userAccessPolicyRepository.AddRangeAsync(userAccessPolicies);
                    }
                }

                // Validate plan if provided
                if (request.PlanId.HasValue)
                {
                    if (!await _planRepository.ExistsAsync(request.PlanId.Value))
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Error.NotFound(
                            code: "CreateTrainingSessionCommandHandler.Handle_PlanNotFound",
                            description: "Plan not found");
                    }
                }

                // Create training session
                var session = new TrainingSession(
                    request.SessionName,
                    accessPolicy.Id,
                    request.PlanState,
                    request.Place,
                    request.WillHappenAt,
                    request.PlanId);

                await _trainingSessionRepository.AddAsync(session);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return session.AsCreated();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(
                    code: "CreateTrainingSessionCommandHandler.Handle_Unexpected",
                    description: ex.Message);
            }
        }
    }



}
