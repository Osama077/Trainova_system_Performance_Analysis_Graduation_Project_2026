using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles.Players;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Profiles
{
    public class PlayerConfiguration : BaseEntityConfiguration<Player>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Player> builder, bool valueGenratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGenratedOnAdd);

            builder.Property(p => p.TShirtName).HasMaxLength(NameLength);
            builder.Property(p => p.PlayerNumber).IsRequired();

            // Positions are enums - BaseEntityConfiguration will attempt to map them to strings if non-flag
            // For Position enum we assume Flags (multiple positions), so keep numeric mapping.

            builder.Property(p => p.DateOfEnrolment)
                .HasDefaultValue("CAST(GETDATE() AS DATE)");

            // Relationships
            builder
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Player>(p=>p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(p => p.PlayerInjuries)
                .WithOne(pi => pi.Player)
                .HasForeignKey(pi => pi.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Players");
        }
    }
}
