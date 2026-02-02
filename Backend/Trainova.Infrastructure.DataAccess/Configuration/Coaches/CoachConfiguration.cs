using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Coaches;

namespace Trainova.Infrastructure.DataAccess.Configuration.Coaches
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.ToTable("Coaches");

            // UserId is the key for Coach (aggregate rooted by User)
            builder.HasKey(c => c.UserId);

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            builder.Property(c => c.Role);
        }
    }
}
