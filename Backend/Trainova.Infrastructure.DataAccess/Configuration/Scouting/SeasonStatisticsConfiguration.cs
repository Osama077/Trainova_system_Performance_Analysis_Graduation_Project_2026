using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Scouting;

namespace Trainova.Infrastructure.DataAccess.Configuration.Scouting
{
    public class SeasonStatisticsConfiguration : IEntityTypeConfiguration<SeasonStatistics>
    {
        public void Configure(EntityTypeBuilder<SeasonStatistics> builder)
        {
            builder.ToTable("ScoutingCandidateSeasonStatistics");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Property(s => s.CandidateId)
                .IsRequired();

            builder.Property(s => s.Season)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.League)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(s => s.Goals)
                .IsRequired();

            builder.Property(s => s.Assists)
                .IsRequired();

            builder.Property(s => s.Matches)
                .IsRequired();

            builder.Property(s => s.PassAccuracy)
                .HasColumnType("real")
                .IsRequired();

            builder.Property(s => s.ShotsPer90)
                .HasColumnType("real")
                .IsRequired();

            builder.Property(s => s.XgPer90)
                .HasColumnType("real")
                .IsRequired();
        }
    }
}
