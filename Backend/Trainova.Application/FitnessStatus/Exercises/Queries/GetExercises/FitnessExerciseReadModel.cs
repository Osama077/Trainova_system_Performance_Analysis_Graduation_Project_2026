using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.Exercises.Queries.GetExercises
{
    public class FitnessExerciseReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EquipmentRequired EquipmentRequired { get; set; }
        public MuscleGroup TargetMuscleGroup { get; set; }
        public ExerciseIntensity DefaultIntensity { get; set; }
        public ExerciseCatagory Category { get; set; }
        public ExerciseType Type { get; set; }

        public int DefaultSets { get; set; }
        public string DefaultRepsOrDuration { get; set; }
        public int? DefaultRestBetweenSetsSec { get; set; }
        public string TypicalLoad { get; set; }
        public int? RecoveryTimeHours { get; set; }

        public string Description { get; set; }
        public string Contraindications { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
