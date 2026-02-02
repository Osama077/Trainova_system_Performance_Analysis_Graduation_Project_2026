using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.PlayerInjuries;

namespace Trainova.Infrastructure.DataAccess.Configuration.PlayerInjuries
{
    public class PlayerInjuryConfiguration : IEntityTypeConfiguration<PlayerInjury>
    {
        public void Configure(EntityTypeBuilder<PlayerInjury> builder)
        {
            builder.ToTable("PlayerInjuries");
            builder.HasKey(pi => pi.Id);

            builder.HasOne(pi => pi.Injury)
                .WithMany()
                .HasForeignKey(pi => pi.InjuryId);

            builder.HasOne(pi => pi.Player)
                .WithMany()
                .HasForeignKey(pi => pi.PlayerId);

            builder.Property(pi => pi.Status);
            builder.Property(pi => pi.HappendAt).IsRequired();
            builder.Property(pi => pi.CreatedAt).IsRequired();
        }
    }
}
