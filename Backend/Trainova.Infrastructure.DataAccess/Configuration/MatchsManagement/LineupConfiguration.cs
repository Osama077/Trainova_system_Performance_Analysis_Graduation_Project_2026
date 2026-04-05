using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MatchsManagement.Lineups;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement
{
    public class LineupConfiguration
        : BaseEntityConfiguration<Lineup>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Lineup> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Lineups");

            builder
                .HasOne(l => l.Match)
                .WithMany(m => m.Lineups)
                .HasForeignKey(l => l.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(l => l.Player)
                .WithMany(p => p.Lineups)
                .HasForeignKey(l => l.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(l => l.Team)
                .WithMany(t => t.Lineups)
                .HasForeignKey(l => l.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.StartingPosition)
                .HasConversion<int>()
                .IsRequired();

            builder.HasIndex(l => l.MatchId);
            builder.HasIndex(l => l.PlayerId);
            builder.HasIndex(l => l.TeamId);
            builder.HasIndex(l => l.StartingPosition);
        }
    }

}
