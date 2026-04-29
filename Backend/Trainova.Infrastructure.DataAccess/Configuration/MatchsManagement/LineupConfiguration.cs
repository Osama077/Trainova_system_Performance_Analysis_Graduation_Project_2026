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
