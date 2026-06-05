using Trainova.Common.SmartEnums;

namespace Trainova.Domain.UserAuth
{
    public class UserRole : SmartEnum<UserRole>
    {
        public string NormalizedName { get; private set; }

        public static readonly UserRole SystemOwner = new UserRole(0, StaticRoleNamesData.SystemOwnerName);

        public static readonly UserRole SystemAdmin = new UserRole(1, StaticRoleNamesData.SystemAdminName);

        public static readonly UserRole Player = new UserRole(2, StaticRoleNamesData.PlayerName);

        public static readonly UserRole TeamStaff = new UserRole(3, StaticRoleNamesData.TeamStaffName);

        public static readonly UserRole HeadCoach = new UserRole(4, StaticRoleNamesData.HeadCoachName);

        public static readonly UserRole AssistantCoach = new UserRole(5, StaticRoleNamesData.AssistantCoachName);

        public static readonly UserRole Doctor = new UserRole(6, StaticRoleNamesData.DoctorName);

        public static readonly UserRole FitnessCoach = new UserRole(7, StaticRoleNamesData.FitnessCoachName);

        public static readonly UserRole TestAccount = new UserRole(8, StaticRoleNamesData.TestAccountName);
        private UserRole(byte id, string name)
            : base(name, id)
        {
            NormalizedName = name.ToLowerInvariant();
        }




        public override string ToString()
        {
            return $"{Name}:{NormalizedName}:{Value}";
        }
    }
    public static class StaticRoleNamesData
    {
        public static readonly string SystemOwnerName = "SystemOwner";

        public static readonly string SystemAdminName = "SystemAdmin";

        public static readonly string PlayerName = "Player";

        public static readonly string TeamStaffName = "TeamStaff";

        public static readonly string HeadCoachName = "HeadCoach";

        public static readonly string AssistantCoachName = "AssistantCoach";

        public static readonly string DoctorName = "Doctor";

        public static readonly string FitnessCoachName = "FitnessCoach";

        public static readonly string TestAccountName = "TestAccount";
        public static readonly string MachineOrServicesBasedRoleName = "MachineOrServicesBased";
    }
}
