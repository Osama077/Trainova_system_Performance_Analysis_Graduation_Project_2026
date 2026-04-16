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
                .HasMaxLength(128);

            builder.Property(ut => ut.UserId)
                .IsRequired();

            builder.Property(ut => ut.TokenType)
                .HasConversion(
                    v => v.Name,
                    v => TokenType.FromName(v)!
                )
                .HasMaxLength(50)
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
