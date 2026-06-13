namespace Trainova.Domain.FitnessStatus.Enums
{
    [Flags]
    public enum EquipmentRequired
    {
        None = 0,
        Barbell = 1 << 0,          // 1
        Dumbbells = 1 << 1,        // 2
        ResistanceBand = 1 << 2,   // 4
        Bodyweight = 1 << 3,       // 8
        Kettlebell = 1 << 4,       // 16
        BoxPlatform = 1 << 5,      // 32
        CableMachine = 1 << 6,     // 64
        Treadmill = 1 << 7,        // 128
        AgilityLadder = 1 << 8,    // 256
        Cones = 1 << 9,            // 512
        MedicineBall = 1 << 10,    // 1024
        NoEquipment = 1 << 11      // 2048
    }

}
