using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.AccessPolicies;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess.Configuration.AccessPolicies
{
    public class PolicyUserConfiguration : IEntityTypeConfiguration<PolicyUser>
    {
        public void Configure(EntityTypeBuilder<PolicyUser> builder)
        {
            builder.ToTable("PolicyUsers");
            builder.HasKey(pu => new { pu.AccessPoliciesId, pu.UserId });
            builder.HasOne(pu => pu.AccessPolicy)
                .WithMany(ap => ap.PolicyUsers)
                .HasForeignKey(pu => pu.AccessPoliciesId);
            builder.HasOne(pu => pu.User);

            builder.Property(pu => pu.HasAttended)
                .IsRequired();
        }
    }

}
