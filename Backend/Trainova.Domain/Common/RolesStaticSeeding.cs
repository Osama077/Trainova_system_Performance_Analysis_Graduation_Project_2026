using Trainova.Domain.UserAuth.Roles;

namespace Trainova.Domain.Common
{
    public static class RolesStaticSeeding
    {
        public static readonly string SystemOwner = "SystemOwner";

        public static readonly string SystemAdmin = "SystemAdmin";

        public static readonly string Player = "Player";

        public static readonly string TeamStaff = "TeamStaff";

        public static readonly string HeadCoach = "HeadCoach";

        public static readonly string AssistantCoach = "AssistantCoach";

        public static readonly string Doctor = "Doctor";

        public static readonly string FitnessCoach = "FitnessCoach";

        public static readonly string TestAccount = "TestAccount";



        public static readonly Role SystemOwnerRole = new Role(0, "SystemOwner");

        public static readonly Role SystemAdminRole =new Role(1, "SystemAdmin");

        public static readonly Role PlayerRole = new Role(2, "Player");

        public static readonly Role TeamStaffRole = new Role(3, "TeamStaff");

        public static readonly Role HeadCoachRole = new Role(4, "HeadCoach");

        public static readonly Role AssistantCoachRole = new Role(5, "AssistantCoach");

        public static readonly Role DoctorRole = new Role(6, "Doctor");

        public static readonly Role FitnessCoachRole = new Role(7, "FitnessCoach");

        public static readonly Role TestAccountRole = new Role(8, "TestAccount");



    }




}
