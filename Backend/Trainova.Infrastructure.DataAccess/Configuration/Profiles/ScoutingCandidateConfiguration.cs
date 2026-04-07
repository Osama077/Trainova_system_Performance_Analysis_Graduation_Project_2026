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
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("ScoutingCandidates");

            builder
                .HasOne(sc => sc.CurrentTeam)
                .WithMany(t => t.ScoutingCandidates)
                .HasForeignKey(sc => sc.CurrentTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(sc => sc.CurrentTeamId);
        }
    }

}
