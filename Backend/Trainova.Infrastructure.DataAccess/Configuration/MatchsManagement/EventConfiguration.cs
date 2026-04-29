using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MatchsManagement.Events;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement
{
    public class EventConfiguration
        : BaseEntityConfiguration<Event>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Event> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Events");



            builder.HasIndex(e => e.MatchId);
            builder.HasIndex(e => e.PlayerId);
            builder.HasIndex(e => e.TeamId);
        }
    }

}
