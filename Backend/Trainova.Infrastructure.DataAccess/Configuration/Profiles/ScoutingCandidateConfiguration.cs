using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles.ScoutingCandidates;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Profiles
{
    public class ScoutingCandidateConfiguration
        : BaseEntityConfiguration<ScoutingCandidate>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<ScoutingCandidate> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("ScoutingCandidates");



            builder.HasIndex(sc => sc.CurrentTeamId);
        }
    }

}
