using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Scouting;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Scouting
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

            // Map Status enum as integer column
            builder.Property(sc => sc.Status)
                .HasConversion<int>()
                .HasColumnName("Status")
                .IsRequired()
                .HasDefaultValue(CandidateStatus.None);

            // Ensure reasonable length for FullName
            builder.Property(sc => sc.FullName).HasMaxLength(200).IsRequired();


            builder.HasIndex(sc => sc.CurrentTeamId);
        }
    }

}
