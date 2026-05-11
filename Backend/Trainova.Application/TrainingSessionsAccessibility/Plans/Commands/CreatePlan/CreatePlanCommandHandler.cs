using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan
{
    public class CreatePlanCommandHandler(
        IPlanRepository _planRepository,
        IAccessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        IUsersRepository _usersRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository)
        : IRequestHandler<CreatePlanCommand, ResultOf<Plan>>
    {
        public async Task<ResultOf<Plan>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(request.PlanName))
                    return Error.Validation("CreatePlanCommandHandler.Handle_InvalidName", "Plan name is required");

                if (string.IsNullOrWhiteSpace(request.PlanGoal))
                    return Error.Validation("CreatePlanCommandHandler.Handle_InvalidGoal", "Plan goal is required");

                if (request.EndDate.HasValue && request.EndDate <= request.StartDate)
                    return Error.Validation("CreatePlanCommandHandler.Handle_InvalidDateRange", "End date must be after start date");

                var creatingNewPolicy = !request.AccessPolicyId.HasValue;

                if (creatingNewPolicy && !(request.UserIds?.Any() ?? false))
                    return Error.Validation("CreateTrainingSession.EmptyUsers", "Users are required when creating a new policy");



                await _unitOfWork.StartTransactionAsync();

                AccessPolicy accessPolicy;

                if (!creatingNewPolicy)
                {
                    accessPolicy = await _accessPolicyRepository.GetByIdAsync(request.AccessPolicyId!.Value);

                    if (accessPolicy is null)
                        return Error.NotFound("CreateTrainingSession.PolicyNotFound", "Access policy not found");
                }
                else
                {
                    accessPolicy = new AccessPolicy(request.PlanName);

                    var users = await _usersRepository.GetByIdsAsync(request.UserIds!);

                    if (users.Count() != request.UserIds!.Count)
                        return Error.NotFound("CreateTrainingSession.UserNotFound", "One or more users not found");

                    var userAccessPolicies = users
                        .Select(u => new UserAccessPolicy(accessPolicy.Id, u.Id, AttendanceStatus.Waiting))
                        .ToList();
                    await _accessPolicyRepository.AddAsync(accessPolicy);

                    await _userAccessPolicyRepository.AddRangeAsync(userAccessPolicies);
                }

                var plan = new Plan(
                    request.PlanName,
                    request.PlanGoal,
                    request.PlanState,
                    accessPolicy.Id,
                    request.StartDate,
                    request.EndDate);

                await _planRepository.AddAsync(plan);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return plan.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("CreatePlanCommandHandler.Handle_Unexpected", ex.Message);
            }
        }
    }

}
