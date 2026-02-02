using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Players;

namespace Trainova.Infrastructure.DataAccess.Configuration.Players
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired();

            builder.Property(p => p.PlayerNumber).IsRequired();
            builder.Property(p => p.TShirtName).IsRequired().HasMaxLength(100);

            // Store MedecalStatus and Position as strings for readability and stability
            builder.Property(p => p.MedecalStatus)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.CurrentPosition)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.PerformanceScore);
            builder.Property(p => p.InjuryRisk);
            builder.Property(p => p.CreatedAt).IsRequired();
        }
    }
}
