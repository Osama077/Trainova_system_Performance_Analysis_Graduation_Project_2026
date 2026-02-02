using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Injuries;

namespace Trainova.Infrastructure.DataAccess.Configuration.Injuries
{
    public class InjuryConfiguration : IEntityTypeConfiguration<Injury>
    {
        public void Configure(EntityTypeBuilder<Injury> builder)
        {
            builder.ToTable("Injuries");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Description).HasMaxLength(2000);
            builder.Property(i => i.AverageRecoveryTime).IsRequired();
            builder.Property(i => i.CreatedAt).IsRequired();
        }
    }
}
