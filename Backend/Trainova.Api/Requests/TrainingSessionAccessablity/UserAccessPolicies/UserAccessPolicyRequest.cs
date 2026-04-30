using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.CreateUserAccessPolicy;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.UpdateUserAccessPolicy;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.UserAccessPolicies
{
    public class CreateUserAccessPolicyRequest
    {
        public Guid AccessPolicyId { get; set; }
        public Guid UserId { get; set; }
        public AttendanceStatus? InitialStatus { get; set; }

        public CreateUserAccessPolicyCommand ToCommand()
        {
            return new CreateUserAccessPolicyCommand(
                AccessPolicyId,
                UserId,
                InitialStatus ?? AttendanceStatus.Waiting);
        }
    }

    public class UpdateUserAccessPolicyRequest
    {
        public AttendanceStatus? Status { get; set; }
        public decimal? DoneScore { get; set; }

        public UpdateUserAccessPolicyCommand ToCommand(Guid id)
        {
            return new UpdateUserAccessPolicyCommand(id, Status, DoneScore);
        }
    }
}
