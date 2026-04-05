using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus.Injuries;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class InjuryConfiguration
        : BaseEntityConfiguration<Injury>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Injury> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Injuries");

            builder
                .HasMany(i => i.PlayerInjuries)
                .WithOne(pi => pi.Injury)
                .HasForeignKey(pi => pi.InjuryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(i => i.InjuryType)
                .HasConversion<string>()
                .HasMaxLength(40);

            builder.HasIndex(i => i.InjuryType);
        }
    }

}
