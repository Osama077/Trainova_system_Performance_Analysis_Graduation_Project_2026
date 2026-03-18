using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.MatchsManagement.Matches
{

    public class Match : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public TrainingSession TrainingSession { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        object IAuditable.Id => Id;

        public Guid? CreatedBy { get; private set; }
    }

}
