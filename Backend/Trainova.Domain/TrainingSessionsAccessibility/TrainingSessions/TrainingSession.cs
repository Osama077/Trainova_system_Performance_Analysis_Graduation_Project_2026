using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions
{
    public class TrainingSession : AuditableEntity<Guid>
    {
        public string TrainingSessionName { get; private set; }
        public Guid? PlanId { get; private set; }
        public Plan? Plan { get; private set; }
        public PlanState SessionState { get; private set; }
        public string? Place { get; private set; }
        public Match? Match { get; private set; }
        public DateTime? HappenedAt { get; private set; }
        public TrainingSession(
            string trainingSessionName,
            Guid? planId,
            PlanState sessionState,
            string? place,
            DateTime? happenedAt,
            Guid? createdBy = null) :base(createdBy)
        {
            TrainingSessionName = trainingSessionName;
            PlanId = planId;
            SessionState = sessionState;
            Place = place;
            HappenedAt = happenedAt;
        }
        private TrainingSession() :base() { }

        public ICollection<UserAccessPolicy> UserAccessPolicies { get; private set; } = new List<UserAccessPolicy>();

    }

}
