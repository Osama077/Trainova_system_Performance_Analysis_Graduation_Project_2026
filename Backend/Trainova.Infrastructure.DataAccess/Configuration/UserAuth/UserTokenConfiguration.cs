using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Infrastructure.DataAccess.Configuration.UserAuth
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.Token)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(ut => ut.UserId)
                .IsRequired();

            // Configure TokenType as a value object with conversion to store as int (Value)
            builder.Property(ut => ut.TokenType)
                .HasConversion(
                    v => v.Value,  // Store the integer value
                    v => TokenType.FromValue(v)!  // Retrieve from value - will not return null for valid values
                )
                .IsRequired();

            builder.Property(ut => ut.IsUsed)
                .IsRequired();

            builder.Property(ut => ut.UsedAt);

            builder.Property(ut => ut.IsRevoked)
                .IsRequired();

            builder.Property(ut => ut.RevokeCause);

            builder.Property(ut => ut.RevokedAt);

            builder.Property(ut => ut.CreatedAt)
                .IsRequired();

            // Foreign key relationship
            builder.HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
