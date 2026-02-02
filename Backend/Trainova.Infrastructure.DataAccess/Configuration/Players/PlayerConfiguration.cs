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

            // MedecalStatus and Position are enums
            builder.Property(p => p.MedecalStatus);
            builder.Property(p => p.CurrentPosition).HasMaxLength(50);

            builder.Property(p => p.PerformanceScore);
            builder.Property(p => p.InjuryRisk);
            builder.Property(p => p.CreatedAt).IsRequired();
        }
    }
}
