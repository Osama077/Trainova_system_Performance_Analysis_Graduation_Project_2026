using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.Profiles.TeamsStaff
{
    public class TeamStaff : AuditableEntity<Guid>
    {
        public User User { get; private set; }

        public string? InsuranceFilesLink { get; private set; }
        public string? ContractFilesLink { get; private set; }
        public TeamStaffRole Role { get; private set; }


        public void UpdateState(string? insuranceFilesLink = null, string? contractFilesLink = null)
        {
            MarkUpdatedNow();

            InsuranceFilesLink = insuranceFilesLink ?? InsuranceFilesLink;
            ContractFilesLink = contractFilesLink ?? ContractFilesLink;
        }

        private TeamStaff():base() { }
        public TeamStaff(
            Guid Id,
            string? insuranceFilesLink,
            string? contractFilesLink,
            TeamStaffRole role,
            Guid? createdBy = null) : base(Id, createdBy)
        {
            InsuranceFilesLink = insuranceFilesLink;
            Role = role;
            ContractFilesLink = contractFilesLink;
        }
    }
}
