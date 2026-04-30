using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateAccessPolicy;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.UpdateAccessPolicy;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.AccessPolicies
{
    public class CreateAccessPolicyRequest
    {
        public string PolicyName { get; set; }

        public CreateAccessPolicyCommand ToCommand()
        {
            return new CreateAccessPolicyCommand(PolicyName);
        }
    }

    public class UpdateAccessPolicyRequest
    {
        public string? PolicyName { get; set; }

        public UpdateAccessPolicyCommand ToCommand(Guid id)
        {
            return new UpdateAccessPolicyCommand(id, PolicyName);
        }
    }
}
