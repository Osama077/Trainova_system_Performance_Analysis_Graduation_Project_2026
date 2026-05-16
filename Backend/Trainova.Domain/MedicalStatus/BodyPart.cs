using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus
{
    [StoreAsString]
    public enum BodyPart
    {
        HeadAndNeck = 1,
        ChestAndTorso,
        LeftArm,
        RightArm,
        LeftLeg,
        RightLeg,
        RightAnkle,
        LeftAnkle,
        Quadriceps
    }
}
