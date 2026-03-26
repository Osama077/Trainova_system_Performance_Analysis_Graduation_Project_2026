using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.Profiles.TeamsStaff
{
    public class TeamStaff : AuditableEntity<Guid>
    {
        public User User { get; private set; }
        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }
        public string? InsuranceFilesLink { get; private set; }
        public string? ContractFilesLink { get; private set; }
        public TeamStaffRole Role { get; private set; }


        public void UpdateState(string? insuranceFilesLink = null, string? contractFilesLink = null)
        {
            InsuranceFilesLink = insuranceFilesLink ?? InsuranceFilesLink;
            ContractFilesLink = contractFilesLink ?? ContractFilesLink;
            MarkUpdatedNow();
        }

        private TeamStaff():base() { }
        public TeamStaff(
            Guid Id,
            Guid teamId,
            string? insuranceFilesLink,
            string? contractFilesLink,
            TeamStaffRole role,
            Guid? createdBy = null) : base(Id, createdBy)
        {
            TeamId = teamId;
            InsuranceFilesLink = insuranceFilesLink;
            Role = role;
            ContractFilesLink = contractFilesLink;
        }
    }
}
