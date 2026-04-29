using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.TrainingSessionsAccessibility
{
    public class AccessPolicyConfiguration
        : BaseEntityConfiguration<AccessPolicy>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<AccessPolicy> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);
            builder.HasOne(x => x.Plan)
                .WithOne()
                .HasForeignKey<AccessPolicy>(a => a.PlanId);
            builder.HasOne(x => x.TrainingSession)
                .WithOne()
                .HasForeignKey<AccessPolicy>(a => a.TrainingSessionId);

            builder.ToTable("AccessPolicies");
        }
        
    }

}
