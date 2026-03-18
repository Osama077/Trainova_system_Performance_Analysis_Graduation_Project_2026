namespace Trainova.Domain.Common.Enums
{
    [Flags]
    public enum Position
    {
        GK = 1,
        CB = 2,
        LB = 4,
        RB = 8,
        CDM = 16,
        CM = 32,
        CAM = 64,
        RW = 128,
        LW = 256,
        ST = 512,
    }
}
