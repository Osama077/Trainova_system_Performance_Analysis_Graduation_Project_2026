using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus.Injuries;
using Trainova.Infrastructure.DataAccess.Common;

namespace Trainova.Infrastructure.DataAccess.MedicalStatus.Injuries
{
    public class InjuryConfiguration : BaseEntityConfiguration<Injury>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Injury> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.Property(i => i.Name).IsRequired().HasMaxLength(NameLength);
            builder.Property(i => i.Description).HasMaxLength(DescriptionLength);

            // InjuryType is nullable enum; it's decorated with JsonStringEnumConverter in domain, we will store it as string
            builder.Property(i => i.InjuryType).HasConversion<string>();

            builder.Property(i => i.AverageRecoveryTime);

            builder.HasMany(i => i.PlayerInjuries).WithOne(pi => pi.Injury).HasForeignKey("InjuryId");

            builder.ToTable("Injuries");
        }
    }
}
