using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.SeasonsAnalyses.PositionBenchmarks;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.SeasonsAnalyses
{
    public class PositionBenchmarkConfiguration
        : BaseEntityConfiguration<PositionBenchmark>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<PositionBenchmark> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("PositionBenchmarks");

            builder.HasIndex(pb => pb.SeasonId);
            builder.HasIndex(pb => pb.CompetitionId);
        }
    }

}
