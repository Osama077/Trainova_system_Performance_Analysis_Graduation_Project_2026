using Trainova.Domain.Common;
using Trainova.Domain.Events;

namespace Trainova.Domain.Activities
{
    public class Activity: IAuditable
    {
        public Guid Id { get; private set; }
        public Guid? EventId { get; private set; }
        public Event? RelatedEvent { get; private set; }
        public ActivityType Type { get; private set; }
        public ManageableBy ManageableBy { get; private set; } = ManageableBy.Owner;
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
        public Guid? Owner { get; private set; }

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
