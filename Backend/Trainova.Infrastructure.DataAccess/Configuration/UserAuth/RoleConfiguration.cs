using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.UserAuth.Roles;

namespace Trainova.Infrastructure.DataAccess.Configuration.UserAuth
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r=>r.Id)
                .IsRequired()
                .ValueGeneratedNever();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(64);
            builder.Property(r => r.NormalizedName).IsRequired().HasMaxLength(64);

        }
    }
}
