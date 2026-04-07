using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.SeasonsAnalyses.ModelScores;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.SeasonsAnalyses
{
    public class ModelScoreConfiguration
        : BaseEntityConfiguration<ModelScore>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<ModelScore> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("ModelScores");

            builder
                .HasOne(ms => ms.Player)
                .WithMany(p => p.ModelScores)
                .HasForeignKey(ms => ms.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ms => ms.Match)
                .WithMany(m => m.ModelScores)
                .HasForeignKey(ms => ms.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ms => ms.PlayerId);
            builder.HasIndex(ms => ms.MatchId);
        }
    }

}
