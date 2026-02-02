using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess.Configuration.Users
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.Token).IsRequired().HasMaxLength(400);

            builder.HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId);

            builder.Property(ut => ut.IsUsed).IsRequired();
            builder.Property(ut => ut.IsRevoked).IsRequired();
            builder.Property(ut => ut.CreatedAt).IsRequired();

            // TokenType is an enum - keep as numeric by default; global config will adjust if annotated
            builder.Property(ut => ut.TokenType);
        }
    }
}
