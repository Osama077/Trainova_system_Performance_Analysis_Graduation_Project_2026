using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.TrainingSessionsAccessibility.Activities
{
    public class Activity: IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public Guid? TrainingSessionId { get; private set; }
        public TrainingSession? RelatedTrainingSession { get; private set; }
        public ActivityType Type { get; private set; }
        public string Name { get; private set; }
        public string Notes { get; private set; }
        public bool IsCompleted { get; private set; }= false;
        public bool IsCancelled { get; private set; } = false;
        public bool IsSkipped { get; private set; } = false;

        public TaskMethod? HowToBeDone { get; private set; } = null;
        public int? Times { get; private set; }
        public TimeSpan? Duration { get; private set; }


        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;


    }
    public enum ActivityType
    {
        MedicalExamination,
        FitnessActivity,
        CoachAssessment,
        TacticalSession
    }
    public enum TaskMethod
    {
        Repetition,
        Duration
    }
}
