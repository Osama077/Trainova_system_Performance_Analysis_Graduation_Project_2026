using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Events;

namespace Trainova.Infrastructure.DataAccess.Configuration.Events
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EventName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Place).HasMaxLength(200);

            builder.Property(e => e.HappenedAt);
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasOne(e => e.Plan)
                .WithMany(p => p.Events)
                .HasForeignKey(e => e.PlanId);

            builder.Property(e => e.ManageableBy);
        }
    }
}
