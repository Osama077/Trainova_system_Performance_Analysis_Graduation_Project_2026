using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.DeletePlan
{
    public class DeletePlanCommandHandler(
        IPlanRepository _planRepository,
        ITrainingSessionRepository _trainingSessionRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeletePlanCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate request
                if (request == null)
                    return Error.Validation(code: "DeletePlanCommandHandler.Handle_NullRequest", description: "Request cannot be null");

                // Start transaction
                await _unitOfWork.StartTransactionAsync();

                // Get existing plan
                var plan = await _planRepository.GetByIdAsync(request.Id);
                if (plan == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.NotFound(
                        code: "DeletePlanCommandHandler.Handle_PlanNotFound",
                        description: "Plan not found");
                }

                // Check if plan has associated training sessions
                if (await _trainingSessionRepository.ExistsAsync(planId: request.Id))
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Error.Conflict(
                        code: "DeletePlanCommandHandler.Handle_Conflict",
                        description: "Cannot delete plan because it has associated training sessions");
                }

                // Delete plan
                await _planRepository.DeleteAsync(plan);

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
                return Error.Unexpected(code: "DeletePlanCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
