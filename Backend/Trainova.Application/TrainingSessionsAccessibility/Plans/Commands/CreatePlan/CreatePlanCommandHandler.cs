using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan
{
    public class CreatePlanCommandHandler(
        IPlanRepository _planRepository,
        IAccsessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreatePlanCommand, ResultOf<Plan>>
    {
        public async Task<ResultOf<Plan>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "CreatePlanCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                if (string.IsNullOrWhiteSpace(request.PlanName))
                    return Error.Validation(code: "CreatePlanCommandHandler.Handle_InvalidPlanName", description: "Plan name is required and cannot be empty");

                if (string.IsNullOrWhiteSpace(request.PlanGoal))
                    return Error.Validation(code: "CreatePlanCommandHandler.Handle_InvalidPlanGoal", description: "Plan goal is required and cannot be empty");

                if (request.EndDate.HasValue && request.EndDate <= request.StartDate)
                    return Error.Validation(code: "CreatePlanCommandHandler.Handle_InvalidDateRange", description: "End date must be after start date");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Validate access policy exists
                var accessPolicy = await _accessPolicyRepository.GetByIdAsync(request.AccessPolicyId);
                if (accessPolicy == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "CreatePlanCommandHandler.Handle_PolicyNotFound",
                        description: "Access policy not found");
                }

                // Create plan
                var plan = new Plan(
                    request.PlanName,
                    request.PlanGoal,
                    PlanState.Active,
                    request.AccessPolicyId,
                    request.StartDate,
                    request.EndDate,
                    _currentUser.Id);

                await _planRepository.AddAsync(plan);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return plan.AsCreated();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "CreatePlanCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
