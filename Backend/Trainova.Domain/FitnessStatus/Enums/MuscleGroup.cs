namespace Trainova.Domain.FitnessStatus.Enums
{
    [Flags]
    public enum MuscleGroup
    {
        None = 0,
        Quadriceps = 1 << 0,   // 1
        Hamstrings = 1 << 1,   // 2
        Glutes = 1 << 2,       // 4
        Calves = 1 << 3,       // 8
        CoreAbs = 1 << 4,      // 16
        HipFlexors = 1 << 5,   // 32
        Chest = 1 << 6,        // 64
        Back = 1 << 7,         // 128
        Shoulders = 1 << 8,    // 256
        Biceps = 1 << 9,       // 512
        Triceps = 1 << 10,     // 1024
        FullBody = 1 << 11     // 2048
    }

}
