using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.ScoutingCandidates;

namespace Trainova.Infrastructure.DataAccess.Configuration.ScoutingCandidates
{
    public class ScoutingCandidateConfiguration : IEntityTypeConfiguration<ScoutingCandidate>
    {
        public void Configure(EntityTypeBuilder<ScoutingCandidate> builder)
        {
            builder.ToTable("ScoutingCandidates");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.FullName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Age).IsRequired();
            builder.Property(s => s.Position).IsRequired();
            builder.Property(s => s.PerformanceScore);
            builder.Property(s => s.InjuryRisk);
        }
    }
}
