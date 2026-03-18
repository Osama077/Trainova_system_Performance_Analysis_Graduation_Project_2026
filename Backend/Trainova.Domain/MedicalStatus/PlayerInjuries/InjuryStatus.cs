using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus.PlayerInjuries
{
    [StoreAsString]
    public enum InjuryStatus
    {
        InHealing = 0,
        InRecovery = 1,
        Ended = 2,
    }
}
