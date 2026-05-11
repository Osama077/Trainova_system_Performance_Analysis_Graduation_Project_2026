using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.UserAuth;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.UserAuth
{
    public class UserConfiguration :BaseEntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder, bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.Property(U => U.IsEmailConfirmed)
                .IsRequired()
                .HasDefaultValue(false);


            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash")
                .HasMaxLength(2400);
            builder
            .HasIndex(u => u.Email)
            .IsUnique();



            builder.Property(p => p.Role)
                .HasConversion(
                    r => r.Name,
                    value => Role.FromName(value.Trim(), false))
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(p => p.Role);


        }
    }
}
