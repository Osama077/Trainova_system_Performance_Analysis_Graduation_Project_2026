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
            builder.Property(r => r.Name).IsRequired().HasMaxLength(64);
            builder.Property(r => r.NormalizedName).IsRequired().HasMaxLength(64);
            // Seed data for roles
            builder.HasData(
                new HashSet<Role>
                    {   new Role(1, "Admin"),
                        new Role(2, "Player"),
                        new Role(3, "TeamStaff"),
                        new Role(4, "headCoach"),
                        new Role(5, "AssistantCoach"),
                        new Role(6, "Doctor"),
                        new Role(7, "FitnessCoach")
                    }
            );
        }
    }
}
