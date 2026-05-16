using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus
{
    [StoreAsString]
    public enum InjuryCause
    {
        Training = 1,
        Match,
        OverUse,
        Collision,
        Unknown
    }
}
