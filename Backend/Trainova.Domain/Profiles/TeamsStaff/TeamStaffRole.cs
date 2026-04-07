using Trainova.Domain.Common;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Profiles.TeamsStaff
{
    [StoreAsString]
    public enum TeamStaffRole
    {
        FitnessCoach = 1,
        AssistantCoach = 2,
        headCoach = 3,
        Doctor = 4
    }
}
