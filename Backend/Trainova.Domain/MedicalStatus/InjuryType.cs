using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus
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
