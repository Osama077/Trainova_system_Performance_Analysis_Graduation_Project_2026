﻿using Trainova.Domain.Common.BaseEntity;
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
        public Guid? AccessPolicyId { get; private set; }
        public AccessPolicy? AccessPolicy { get; private set; }
        public PlanState SessionState { get; private set; }
        public string? Place { get; private set; }
        public Match? Match { get; private set; }
        public DateTime? HappenedAt { get; private set; }
        public TrainingSession(
            string trainingSessionName,
            Guid? accessPolicyId,
            PlanState sessionState,
            string? place = null,
            DateTime? happenedAt = null,
            Guid? planId = null,
            Guid? createdBy = null) :base(Guid.NewGuid(),createdBy)
        {
            TrainingSessionName = trainingSessionName;
            PlanId = planId;
            AccessPolicyId = accessPolicyId;
            SessionState = sessionState;
            Place = place;
            HappenedAt = happenedAt;
        }
        // ...existing code...
        private TrainingSession() :base() { }

        public ICollection<UserAccessPolicy> UserAccessPolicies { get; private set; } = new List<UserAccessPolicy>();

        public void Update(string? sessionName = null, string? place = null, DateTime? happenedAt = null, PlanState? state = null)
        {
            if (!string.IsNullOrWhiteSpace(sessionName))
                TrainingSessionName = sessionName;

            if (!string.IsNullOrWhiteSpace(place))
                Place = place;

            if (happenedAt.HasValue)
                HappenedAt = happenedAt;

            if (state.HasValue)
                SessionState = state.Value;
        }
    }

}
