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

            builder
                .HasOne(e => e.Match)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Player)
                .WithMany(p => p.Events)
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Team)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.MatchId);
            builder.HasIndex(e => e.PlayerId);
            builder.HasIndex(e => e.TeamId);
        }
    }

}
