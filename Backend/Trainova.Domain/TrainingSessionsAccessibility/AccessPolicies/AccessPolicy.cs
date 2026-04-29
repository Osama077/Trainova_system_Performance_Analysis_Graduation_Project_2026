using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class AccessPolicy : AuditableEntity<Guid>
    {
        public string? PolicyName { get; private set; } = null;
        public Guid? PlanId { get; private set; } = null;
        public Plan Plan { get; private set; } = null;
         public Guid? TrainingSessionId { get; private set; } = null;
         public TrainingSession TrainingSession { get; private set; } = null;
        
        public ICollection<UserAccessPolicy> PolicyUsers { get; private set; } = [];
        private AccessPolicy() : base()
        {}
        public AccessPolicy(Guid? planId = null, Guid? trainingSessionId = null, string? policyName = null, Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            PlanId = planId;
            TrainingSessionId=  trainingSessionId;
            PolicyName = policyName;
        }
        
        
    }

}
