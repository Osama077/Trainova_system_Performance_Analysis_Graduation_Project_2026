using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.TrainingSessionsAccessibility.Plans
{
    public class Plan : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public string PlanName { get; private set; }
        public string PlanGoul { get; private set; }
        public PlanState State { get; private set; }
        public Guid AccessPolicyId { get; private set; }
        public AccessPolicy AccessPolicy { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }

        public ICollection<TrainingSession> TrainingSessions { get; private set; } = new List<TrainingSession>();

        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

    }


}
