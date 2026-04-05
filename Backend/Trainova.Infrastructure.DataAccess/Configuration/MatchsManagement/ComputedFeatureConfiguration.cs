using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MatchsManagement.ComputedFeatures;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement
{
    public class ComputedFeatureConfiguration
        : BaseEntityConfiguration<ComputedFeature>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<ComputedFeature> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("ComputedFeatures");

            builder
                .HasOne(cf => cf.Player)
                .WithMany(p => p.ComputedFeatures)
                .HasForeignKey(cf => cf.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(cf => cf.Match)
                .WithMany(m => m.ComputedFeatures)
                .HasForeignKey(cf => cf.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cf => cf.PlayerId);
            builder.HasIndex(cf => cf.MatchId);
        }
    }

}
