using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Activities;

namespace Trainova.Infrastructure.DataAccess.Configuration.Activities
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Notes).HasMaxLength(2000);

            builder.Property(a => a.IsCompleted).IsRequired();
            builder.Property(a => a.IsCancelled).IsRequired();
            builder.Property(a => a.IsSkipped).IsRequired();

            // Configure relationship to Event via RelatedEvent navigation; Event has no ActivityEvents collection
            builder.HasOne(a => a.RelatedEvent)
                .WithMany()
                .HasForeignKey(a => a.EventId)
                .IsRequired(false);

            // Enums and additional properties: store ActivityType and TaskMethod as strings for readability
            builder.Property(a => a.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.HowToBeDone)
                .HasConversion<string>();

            builder.Property(a => a.ManageableBy).IsRequired();

            // Optional task details
            builder.Property(a => a.Times);
            builder.Property(a => a.Duration);

            // Auditable
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.LastUpdate);
            builder.Property(a => a.Owner);
        }
    }
}
