using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Scouting;

namespace Trainova.Infrastructure.DataAccess.Configuration.Scouting
{
    /// <summary>
    /// EF Core configuration for the <see cref="CandidateMatch"/> entity.
    /// The corresponding DB table is "CandidateMatch".
    /// </summary>
    public class CandidateMatchConfiguration : IEntityTypeConfiguration<CandidateMatch>
    {
        public void Configure(EntityTypeBuilder<CandidateMatch> builder)
        {
            builder.ToTable("ScoutingCandidateMatch");

            builder.HasKey(m => m.Id);

            // Id is assigned by the domain constructor — no DB-generated value needed.
            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.CandidateId)
                .IsRequired();

            builder.Property(m => m.MatchDate)
                .IsRequired();

            // Match name: e.g. "Al Ahly vs Zamalek" — capped at 400 chars
            builder.Property(m => m.MatchName)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(m => m.Goals)
                .IsRequired();

            builder.Property(m => m.Assists)
                .IsRequired();

            // Rating is a float (0.0 – 10.0); stored as SQL 'real'
            builder.Property(m => m.Rating)
                .HasColumnType("real")
                .IsRequired();

            // ScoutNotes can be a detailed paragraph — capped at 1200 chars
            builder.Property(m => m.ScoutNotes)
                .HasMaxLength(1200)
                .IsRequired(false);


        }
    }
}
