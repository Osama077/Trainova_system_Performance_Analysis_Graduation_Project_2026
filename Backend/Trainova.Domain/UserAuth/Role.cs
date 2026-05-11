using Trainova.Common.SmartEnums;

namespace Trainova.Domain.UserAuth
{
    public class Role : SmartEnum<Role>
    {
        public string NormalizedName { get; private set; }

        public static readonly Role SystemOwner = new Role(0, StaticRoleNamesData.SystemOwnerName);

        public static readonly Role SystemAdmin = new Role(1, StaticRoleNamesData.SystemAdminName);

        public static readonly Role Player = new Role(2, StaticRoleNamesData.PlayerName);

        public static readonly Role TeamStaff = new Role(3, StaticRoleNamesData.TeamStaffName);

        public static readonly Role HeadCoach = new Role(4, StaticRoleNamesData.HeadCoachName);

        public static readonly Role AssistantCoach = new Role(5, StaticRoleNamesData.AssistantCoachName);

        public static readonly Role Doctor = new Role(6, StaticRoleNamesData.DoctorName);

        public static readonly Role FitnessCoach = new Role(7, StaticRoleNamesData.FitnessCoachName);

        public static readonly Role TestAccount = new Role(8, StaticRoleNamesData.TestAccountName);
        private Role(byte id,string name)
            : base(name,id)
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
    }
}
