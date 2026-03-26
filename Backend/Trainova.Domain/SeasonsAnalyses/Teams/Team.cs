using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.SeasonsAnalyses.Teams
{
    public class Team :AuditableEntity<Guid>
    {

        public string? TeamName { get; private set; }

        public string? Country { get; private set; }


        private Team():base() { } 

        public Team(
            string? teamName,
            string? country,
            Guid? createdBy = null) : base(createdBy)
        {
            TeamName = teamName;
            Country = country;

        }

        public void Update(
            string? teamName = null,
            string? country = null)
        {
            TeamName = teamName ?? TeamName;
            Country = country ?? Country;
            MarkUpdatedNow();
        }
    }

}
