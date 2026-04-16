using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.Outbox;

namespace Trainova.Infrastructure.DataAccess.Configuration.Common
{
    public class EmailOutboxConfiguration : IEntityTypeConfiguration<EmailOutbox>
    {
        public void Configure(EntityTypeBuilder<EmailOutbox> builder)
        {
            builder.ToTable("EmailOutboxes");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.UserEmail).IsRequired().HasMaxLength(200);
            builder.Property(e => e.EmailType).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Token).HasMaxLength(12);
            builder.Property(e => e.IsSent).IsRequired();
            builder.Property(e => e.RetryCount).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
        }
    }
}
