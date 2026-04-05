using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class PlayerInjuryConfiguration :BaseEntityConfiguration<PlayerInjury>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PlayerInjury> builder, bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);
            builder.ToTable("PlayerInjuries");
            builder.Property(p => p.Cause)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .IsRequired();

        }
    }
}
