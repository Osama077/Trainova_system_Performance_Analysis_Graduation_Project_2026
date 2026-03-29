using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles.Players;
using Trainova.Infrastructure.DataAccess.Common;

namespace Trainova.Infrastructure.DataAccess.Profiles.Players
{
    public class PlayerConfiguration : BaseEntityConfiguration<Player>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.TShirtName).HasMaxLength(NameLength);
            builder.Property(p => p.PlayerNumber).IsRequired();

            // Positions are enums - BaseEntityConfiguration will attempt to map them to strings if non-flag
            // For Position enum we assume Flags (multiple positions), so keep numeric mapping.

            // DateOnly support: map to DateTime using strongly typed Property lambda so HasConversion accepts lambdas
            builder.Property(p => p.DateOfEnrolment)
                .HasConversion(
                    d => d == default ? DateTime.MinValue : d.ToDateTime(TimeOnly.MinValue),
                    dt => DateOnly.FromDateTime(dt));

            // Relationships
            builder.HasOne(p => p.User).WithOne().HasForeignKey<Player>("UserId");
            builder.HasMany(p => p.PlayerInjuries).WithOne(pi => pi.Player).HasForeignKey("PlayerId");

            builder.ToTable("Players");
        }
    }
}
