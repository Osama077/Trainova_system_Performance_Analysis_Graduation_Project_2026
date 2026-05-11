using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommandHandler(
        IPlanRepository _planRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdatePlanCommand, ResultOf<Plan>>
    {
        public async Task<ResultOf<Plan>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing plan
                var plan = await _planRepository.GetByIdAsync(request.Id);
                if (plan == null)
                {
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
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "UpdatePlanCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
