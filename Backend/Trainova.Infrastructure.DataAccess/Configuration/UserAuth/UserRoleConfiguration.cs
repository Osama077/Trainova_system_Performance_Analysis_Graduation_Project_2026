using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.UserAuth.UserRoles;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.UserAuth
{
    public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder, bool valueGenratedOnAdd = false)
        {
            builder.ToTable("UserRoles");

            base.ConfigureEntity(builder, valueGenratedOnAdd);

            builder.HasKey(k => new { k.RoleId, k.UserId });

            builder.HasOne(x => x.Role)
                   .WithMany()
                   .HasForeignKey(ur=>ur.RoleId);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(ur => ur.UserId);
        }
    }
}
