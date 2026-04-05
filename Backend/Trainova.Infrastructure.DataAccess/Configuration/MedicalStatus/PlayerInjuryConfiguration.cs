using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class PlayerInjuryConfiguration
        : BaseEntityConfiguration<PlayerInjury>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<PlayerInjury> builder,
            bool valueGeneratedOnAdd = true)
        {
            // Always call base first
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            //----------------------------------------
            // Table
            //----------------------------------------

            builder.ToTable("PlayerInjuries");

            //----------------------------------------
            // Relationships
            //----------------------------------------

            builder
                .HasOne(pi => pi.Player)
                .WithMany(p => p.PlayerInjuries)
                .HasForeignKey(pi => pi.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pi => pi.Injury)
                .WithMany(i => i.PlayerInjuries)
                .HasForeignKey(pi => pi.InjuryId)
                .OnDelete(DeleteBehavior.Cascade);

            //----------------------------------------
            // Enums
            //----------------------------------------

            builder
                .Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder
                .Property(p => p.Cause)
                .HasConversion<string>()
                .HasMaxLength(30);

            //----------------------------------------
            // Defaults
            //----------------------------------------

            builder
                .Property(p => p.IsNew)
                .HasDefaultValue(false);

            //----------------------------------------
            // Indexes (useful for queries)
            //----------------------------------------

            builder
                .HasIndex(p => p.PlayerId);

            builder
                .HasIndex(p => p.InjuryId);

            builder
                .HasIndex(p => p.Status);
        }
    }


}
