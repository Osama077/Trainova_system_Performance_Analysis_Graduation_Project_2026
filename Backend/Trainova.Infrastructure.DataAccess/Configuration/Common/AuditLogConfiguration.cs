using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Infrastructure.DataAccess.Configuration.Common
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
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.OldValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.UserId);

            builder.Property(a => a.From)
                .IsRequired();

            builder.Property(a => a.ChangedAt)
                .IsRequired();

            builder.Property(a => a.IsRecovered)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(a => a.RecoveredAt)
                .IsRequired(false);
            builder.Property(a => a.RecoveredByUserId)
                .IsRequired(false);

            builder.ToTable("AuditLogs");
        }
    }
}
