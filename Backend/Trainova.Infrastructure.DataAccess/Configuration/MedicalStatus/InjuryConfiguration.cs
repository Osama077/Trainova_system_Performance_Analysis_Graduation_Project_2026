using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus.Injuries;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class InjuryConfiguration : BaseEntityConfiguration<Injury>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Injury> builder, bool valueGenratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGenratedOnAdd);

            builder.Property(i => i.Name).IsRequired().HasMaxLength(NameLength);
            builder.Property(i => i.Description).HasMaxLength(DescriptionLength);



            builder.Property(i => i.AverageRecoveryTime);

            builder.HasMany(i => i.PlayerInjuries).WithOne(pi => pi.Injury).HasForeignKey(x=>x.InjuryId);

            builder.ToTable("Injuries");
            builder.Property(x => x.InjuryType)
               .HasConversion(
                   v => v.ToString(), // Enum -> string
                   v => (InjuryType?)Enum.Parse(typeof(InjuryType), v)
               )
                .HasMaxLength(50); // Optional, لو عايز تحدد طول العمود
            // InjuryType is nullable enum; it's decorated with JsonStringEnumConverter in domain, we will store it as string
            builder.Property(i => i.InjuryType).HasConversion<string?>();
        }
    }
}
