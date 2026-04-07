using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.TrainingSessionsAccessibility
{
    public class UserAccessPolicyConfiguration
        : BaseEntityConfiguration<UserAccessPolicy>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<UserAccessPolicy> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("UserAccessPolicies");

            builder
                .HasOne(uap => uap.AccessPolicy)
                .WithMany(ap => ap.PolicyUsers)
                .HasForeignKey(uap => uap.AccessPoliciesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(uap => uap.User)
                .WithMany()
                .HasForeignKey(uap => uap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(uap => uap.AccessPoliciesId);
            builder.HasIndex(uap => uap.UserId);
        }
    }

}
