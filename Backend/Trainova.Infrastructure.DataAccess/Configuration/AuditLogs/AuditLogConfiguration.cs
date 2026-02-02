using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.AuditLogs;

namespace Trainova.Infrastructure.DataAccess.Configuration.AuditLogs
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.EntityId).IsRequired().HasMaxLength(200);

            // AuditActionType is annotated with [StoreAsString] in the domain, store as string for readability
            builder.Property(a => a.Action)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.OldValues).HasMaxLength(4000);

            builder.Property(a => a.From).IsRequired();
            builder.Property(a => a.ChangedAt).IsRequired();
            builder.Property(a => a.IsRecovered).IsRequired();
            builder.Property(a => a.RecoveredAt);
            builder.Property(a => a.UserId);
        }
    }
}
