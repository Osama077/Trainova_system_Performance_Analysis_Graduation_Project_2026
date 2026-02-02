using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.AccessPolicies;

namespace Trainova.Infrastructure.DataAccess.Configuration.AccessPolicies
{
    public class AccessPolicyConfguration : IEntityTypeConfiguration<AccessPolicy>
    {
        public void Configure(EntityTypeBuilder<AccessPolicy> builder)
        {
            builder.ToTable("AccessPolicies");
            builder.HasKey(ap => ap.Id);
            builder.Property(ap => ap.PolicyName)
                .HasMaxLength(100);
            builder.HasMany(ap => ap.PolicyUsers)
                .WithOne(pu => pu.AccessPolicy)
                .HasForeignKey(pu => pu.AccessPoliciesId);
        }
    }

}
