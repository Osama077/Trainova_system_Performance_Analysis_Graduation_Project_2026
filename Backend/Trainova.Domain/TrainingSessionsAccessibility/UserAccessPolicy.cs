using System.Text.Json.Serialization;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.TrainingSessionsAccessibility.Events;
using Trainova.Domain.UserAuth;

namespace Trainova.Domain.TrainingSessionsAccessibility
{
    public class UserAccessPolicy : AuditableEntity<Guid>
    {
        public Guid AccessPoliciesId { get; private set; }
        [JsonIgnore]
        public AccessPolicy AccessPolicy { get; private set; }
        public Guid UserId { get; private set; }
        [JsonIgnore]
        public User User { get; private set; }
        public AttendanceStatus AttendanceState { get; private set; }
        public decimal DoneScore { get; private set; } = 0;
        [JsonIgnore]
        public SessionMovement? SessionMovement { get; private set; }

        public UserAccessPolicy(
            Guid accessPoliciesId,
            Guid userId,
            AttendanceStatus hasAttended = AttendanceStatus.Waiting,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            AccessPoliciesId = accessPoliciesId;
            UserId = userId;
            AttendanceState = hasAttended;
        }

        public void AddNotification(TrainingSession trainingSession)
        {
            AddDomainEvent(new SessionUserAccessPolicyCreatedEvent(
                UserId,
                AccessPoliciesId,
                trainingSession.TrainingSessionName,
                trainingSession.HappenedAt,
                trainingSession.Place));
        }
        public void AddNotification(Plan plan)
        {
            AddDomainEvent(new PlanUserAccessPolicyCreatedEvent(
                UserId,
                AccessPoliciesId,
                plan.PlanName,
                plan.StartDate)
                );
        }


        // obsolete
        /*
        public UserAccessPolicy(
            TrainingSession trainingSession,
            Guid userId,
            AttendanceStatus hasAttended = AttendanceStatus.Waiting,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            AccessPoliciesId = trainingSession.AccessPolicyId;
            UserId = userId;
            AttendanceState = hasAttended;

            AddDomainEvent(new SessionUserAccessPolicyCreatedEvent(
                userId,
                trainingSession.AccessPolicyId,
                trainingSession.TrainingSessionName,
                trainingSession.HappenedAt,
                trainingSession.Place));
        }
        public UserAccessPolicy(
            Plan plan,
            Guid userId,
            AttendanceStatus hasAttended = AttendanceStatus.Waiting,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            AccessPoliciesId = plan.AccessPolicyId;
            UserId = userId;
            AttendanceState = hasAttended;

            AddDomainEvent(new PlanUserAccessPolicyCreatedEvent(
                userId,
                plan.AccessPolicyId,
                plan.PlanName,
                plan.StartDate));
        }
        */


        private UserAccessPolicy() : base() { }
        public void UpdateState(AttendanceStatus hasAttended, decimal doneScore = 100)
        {
            if (doneScore > 100 || doneScore < 0)
                throw new DomainException("done score value out of range");
            DoneScore = doneScore;
            AttendanceState = hasAttended;
        }

    }
}
