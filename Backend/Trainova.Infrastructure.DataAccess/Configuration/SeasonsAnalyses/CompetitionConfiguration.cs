using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.SeasonsAnalyses.Seasons;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.SeasonsAnalyses
{
    public class CompetitionConfiguration
        : BaseEntityConfiguration<Competition>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Competition> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Competitions");

            builder.HasIndex(c => c.SeasonId);
        }
    }

}
