using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions
{
    public class TrainingSession : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public string TrainingSessionName { get; private set; }
        public Guid? PlanId { get; private set; }
        public Plan? Plan { get; private set; }
        public PlanState SessionState { get; private set; }
        public string? Place { get; private set; }
        public DateTime? HappenedAt { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

        public ICollection<PolicyUser> PolicyUsers { get; private set; } = new List<PolicyUser>();

    }

}
