using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.TrainingSessionsAccessibility.Plans
{
    public class Plan : AuditableEntity<Guid>
    {
        public string PlanName { get; private set; }
        public string PlanGoul { get; private set; }
        public PlanState State { get; private set; }
        public Guid AccessPolicyId { get; private set; }
        public AccessPolicy AccessPolicy { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }



        public ICollection<TrainingSession> TrainingSessions { get; private set; } = new List<TrainingSession>();
        private Plan():base(){}
        public Plan(
            string planName,
            string planGoul,
            PlanState state,
            Guid accessPolicyId,
            DateTime startDate,
            DateTime? endDate,
            Guid? createdBy = null) : base(createdBy)
        {
            PlanName = planName;
            PlanGoul = planGoul;
            State = state;
            AccessPolicyId = accessPolicyId;
            StartDate = startDate;
            EndDate = endDate;
        }


    }


}
