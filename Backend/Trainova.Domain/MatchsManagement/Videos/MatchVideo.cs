using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.MatchsManagement.Videos
{
    public class MatchVideo : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public string VideoUrl { get; private set; }
        public Guid? RelatedTrainingSessionId { get; private set; }
        public TrainingSession? RelatedTrainingSession { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;


    }
}
