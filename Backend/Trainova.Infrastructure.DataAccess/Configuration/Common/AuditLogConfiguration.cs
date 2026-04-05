using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Infrastructure.DataAccess.Configuration.Common
{
    public class AuditLogConfiguration
        : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder
                .Property(a => a.Action)
                .HasConversion<string>()
                .HasMaxLength(40);

            builder
                .Property(a => a.IsRecovered)
                .HasDefaultValue(false);

            builder.HasIndex(a => a.EntityName);

            builder.HasIndex(a => a.EntityId);

            builder.HasIndex(a => a.Action);

            builder.HasIndex(a => a.ChangedAt);

            builder.HasIndex(a => a.UserId);

            builder.HasIndex(a => a.IsRecovered);
        }
    }

}
