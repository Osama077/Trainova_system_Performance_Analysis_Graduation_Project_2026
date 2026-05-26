using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetUserAccessPolicy
{
    public class GetUserAccessPolicyDetailsQueryHandler(IUserAccessPolicyRepository _userAccessPolicyRepository)
        : IRequestHandler<GetUserAccessPolicyDetailsQuery, ResultOf<IEnumerable<UserAccessDetailes>>>
    {
        public async Task<ResultOf<IEnumerable<UserAccessDetailes>>> Handle(
            GetUserAccessPolicyDetailsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var details = await _userAccessPolicyRepository.GetUserAccessPolicyDetails(request.PolicyId);

                return details.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetUserAccessPolicyDetailsQueryHandler.Handle_Failure",
                    description: ex.Message
                );
            }
        }
    }
}
