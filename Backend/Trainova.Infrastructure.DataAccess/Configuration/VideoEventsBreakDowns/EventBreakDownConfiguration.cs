using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.VideoEventsBreakDowns;

namespace Trainova.Infrastructure.DataAccess.Configuration.VideoEventsBreakDowns
{
    public class EventBreakDownConfiguration : IEntityTypeConfiguration<EventBreakDown>
    {
        public void Configure(EntityTypeBuilder<EventBreakDown> builder)
        {
            builder.ToTable("EventBreakDowns");

            // Composite key PlayerId + EventId
            builder.HasKey(eb => new { eb.PlayerId, eb.EventId });

            builder.Property(eb => eb.EventType)
                .HasConversion<string>()
                .IsRequired();

            // MicroEventContent is [Owned] so EF will map it as owned type (no explicit HasKey)
        }
    }
}
