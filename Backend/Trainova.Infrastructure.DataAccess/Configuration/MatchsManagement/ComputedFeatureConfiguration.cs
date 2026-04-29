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



            builder.HasIndex(cf => cf.PlayerId);
            builder.HasIndex(cf => cf.MatchId);
        }
    }

}
