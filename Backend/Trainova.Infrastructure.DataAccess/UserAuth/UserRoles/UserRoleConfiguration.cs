using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.UserAuth.UserRoles;
using Trainova.Infrastructure.DataAccess.Common;

namespace Trainova.Infrastructure.DataAccess.UserAuth.UserRoles
{
    public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
        {
            // Configure owned UserRoleId and map its properties to clean column names
            builder.OwnsOne(typeof(object), "Id", id =>
            {
                id.Property<int>("RoleId");
                id.Property<System.Guid>("UserId");

                id.Property<byte>("RoleId").HasColumnName("RoleId");
                id.Property<System.Guid>("UserId").HasColumnName("UserId");

            });

            builder.HasKey("RoleId", "UserId");

            builder.HasOne(typeof(object), "Role").WithMany().HasForeignKey("RoleId");
            builder.HasOne(typeof(object), "User").WithMany("Roles").HasForeignKey("UserId");

            builder.ToTable("UserRoles");
        }
    }
}
