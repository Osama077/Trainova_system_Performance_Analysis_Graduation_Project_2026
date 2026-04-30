using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class UserAccessPolicy : AuditableEntity<Guid>
    {
        public Guid AccessPoliciesId { get; private set; }
        public AccessPolicy AccessPolicy { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public AttendanceStatus AttendanceState { get; private set; }
        public decimal DoneScore { get; private set; } = 0;

        public UserAccessPolicy(
            Guid accessPoliciesId,
            Guid userId,
            AttendanceStatus hasAttended = AttendanceStatus.Waiting,
            Guid? createdBy = null) :base(Guid.NewGuid(), createdBy)
        {
            AccessPoliciesId = accessPoliciesId;
            UserId = userId;
            AttendanceState = hasAttended;
        }
        private UserAccessPolicy() :base() { }
        public void UpdateState(AttendanceStatus hasAttended, decimal doneScore = 100)
        {
            if (doneScore > 100 || doneScore < 0)
                throw new DomainException("done score value out of range");
            DoneScore = doneScore;
            AttendanceState = hasAttended;
        }

    }
}
