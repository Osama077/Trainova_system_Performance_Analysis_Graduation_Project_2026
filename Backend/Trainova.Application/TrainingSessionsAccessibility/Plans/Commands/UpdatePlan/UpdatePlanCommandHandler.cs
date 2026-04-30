using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommandHandler(
        IPlanRepository _planRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<UpdatePlanCommand, ResultOf<Plan>>
    {
        public async Task<ResultOf<Plan>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "UpdatePlanCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                if (request.EndDate.HasValue && request.StartDate.HasValue && request.EndDate <= request.StartDate)
                    return Error.Validation(code: "UpdatePlanCommandHandler.Handle_InvalidDateRange", description: "End date must be after start date");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing plan
                var plan = await _planRepository.GetByIdAsync(request.Id);
                if (plan == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "UpdatePlanCommandHandler.Handle_PlanNotFound",
                        description: "Plan not found");
                }

                // Update plan
                plan.Update(request.PlanName, request.PlanGoal, request.StartDate, request.EndDate);

                await _planRepository.UpdateAsync(plan);

                // Save and commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return plan.AsNoContent();
            }
            catch (DomainException ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                try { await _unitOfWork.RollbackTransactionAsync(); } catch { /* swallow rollback errors */ }
                return Error.Unexpected(code: "UpdatePlanCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
