using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Queries.GetExercisesBySessionId
{
    public class FitnessSessionExercisesReadModel : IHasId<Guid>
    {
        // Junction Table Properties
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public string RepsOrDuration { get; set; } = string.Empty;
        public int? RestTimeSec { get; set; }
        public string LoadDetails { get; set; } = string.Empty;
        public ExerciseIntensity Intensity { get; set; }
        public int? Rounds { get; set; }
        public int? ActiveTimeSec { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }

        // Exercise Table Properties
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public EquipmentRequired EquipmentRequired { get; set; }
        public MuscleGroup TargetMuscleGroup { get; set; }
        public ExerciseIntensity DefaultIntensity { get; set; }
        public ExerciseCatagory Category { get; set; }
        public ExerciseType Type { get; set; }

        // Session Table Properties
        public string TrainingSessionName { get; set; } = string.Empty;
        public Guid? PlanId { get; set; }
        public Guid AccessPolicyId { get; set; }
        public SessionType SessionType { get; set; }
        public PlanState SessionState { get; set; }
        public string? Place { get; set; }
        public DateTime? HappenedAt { get; set; }
    }
}