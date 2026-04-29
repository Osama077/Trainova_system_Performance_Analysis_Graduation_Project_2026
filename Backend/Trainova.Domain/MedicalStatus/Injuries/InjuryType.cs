using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus.Injuries
{
    [StoreAsString]
    public enum InjuryType
    {
        Muscular = 1,
        Bone,
        Joint,
        Ligament,
        Other
    }
}
