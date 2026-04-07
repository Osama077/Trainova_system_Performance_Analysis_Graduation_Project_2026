using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.SeasonsAnalyses
{
    public class TeamConfiguration
        : BaseEntityConfiguration<Team>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Team> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Teams");
        }
    }

}
