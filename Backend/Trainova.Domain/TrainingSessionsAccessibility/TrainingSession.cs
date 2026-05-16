using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Domain.TrainingSessionsAccessibility
{
    public class TrainingSession : AuditableEntity<Guid>
    {
        public string TrainingSessionName { get; private set; }
        public Guid? PlanId { get; private set; }
        public Plan? Plan { get; private set; }
        public Guid AccessPolicyId { get; private set; }
        public AccessPolicy? AccessPolicy { get; private set; }
        public SessionType SessionType { get; private set; }
        public PlanState SessionState { get; private set; }
        public string? Place { get; private set; }
        public Match? Match { get; private set; }
        public DateTime? HappenedAt { get; private set; }
        public TrainingSession(
            string trainingSessionName,
            Guid accessPolicyId,
            PlanState sessionState,
            SessionType sessionType,
            string? place = null,
            DateTime? happenedAt = null,
            Guid? planId = null,
            Guid? createdBy = null) :base(Guid.NewGuid(),createdBy)
        {
            TrainingSessionName = trainingSessionName;
            PlanId = planId;
            AccessPolicyId = accessPolicyId;
            SessionState = sessionState;
            SessionType = sessionType;
            Place = place;
            HappenedAt = happenedAt;
        }
        // ...existing code...
        private TrainingSession() :base() { }


        public void Update(
            string? sessionName = null,
            string? place = null,
            PlanState? planState = null,
            DateTime? happenedAt = null
            )
        {
            MarkUpdatedNow();
            if (!string.IsNullOrWhiteSpace(sessionName))
                TrainingSessionName = sessionName;

            if (!string.IsNullOrWhiteSpace(place))
                Place = place;

            if(planState.HasValue)
                SessionState = planState.Value;


            if (happenedAt.HasValue)
                HappenedAt = happenedAt;

        }
    }

}
