using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement
{
    public class MatchConfiguration
        : BaseEntityConfiguration<Match>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Match> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Matches");





            builder.HasOne(m => m.TrainingSession)
                .WithOne(ts => ts.Match)
                .HasForeignKey<Match>(m => m.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.CompetitionId);
            builder.HasIndex(m => m.HomeTeamId);
            builder.HasIndex(m => m.AwayTeamId);
            builder.HasIndex(m => m.MatchDate);
        }
    }

}
