using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Infrastructure.DataAccess.Common
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.EntityId)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Action)
                .IsRequired();

            builder.Property(a => a.OldValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.UserId);

            builder.Property(a => a.From)
                .IsRequired();

            builder.Property(a => a.ChangedAt)
                .IsRequired();

            builder.Property(a => a.IsRecovered)
                .IsRequired();

            builder.Property(a => a.RecoveredAt);
            builder.Property(a => a.RecoveredByUserId);

            builder.ToTable("AuditLogs");
        }
    }
}
