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
        IAccessPolicyRepository _accessPolicyRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeletePlanCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Get plan أولاً (مش محتاج transaction لسه)
                var plan = await _planRepository.GetByIdAsync(request.Id);

                if (plan is null)
                    return Error.NotFound("DeletePlanCommandHandler.Handle_PlanNotFound", "Plan not found");

                // ❌ لو فيه sessions
                if (await _trainingSessionRepository.ExistsAsync(planId: request.Id))
                    return Error.Conflict("DeletePlanCommandHandler.Handle_Conflict", "Cannot delete plan because it has associated training sessions");

                await _unitOfWork.StartTransactionAsync();

                var accessPolicyId = plan.AccessPolicyId;

                await _planRepository.DeleteAsync(plan);

                var plansCount = await _planRepository.CountByAccessPolicyIdAsync(accessPolicyId);
                var sessionsCount = await _trainingSessionRepository.CountByAccessPolicyIdAsync(accessPolicyId);

                if (plansCount == 0 && sessionsCount == 0)
                {
                    var userPolicies = await _userAccessPolicyRepository.GetAllAsync(accessPolicyId);
                    if (userPolicies.Any())
                        await _userAccessPolicyRepository.DeleteRangeAsync(userPolicies);

                    var accessPolicy = await _accessPolicyRepository.GetByIdAsync(accessPolicyId);
                    if (accessPolicy is not null)
                        await _accessPolicyRepository.DeleteAsync(accessPolicy);
                }


                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return Done.done.AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("DeletePlanCommandHandler.Handle_Unexpected", ex.Message);
            }
        }
    }

}
