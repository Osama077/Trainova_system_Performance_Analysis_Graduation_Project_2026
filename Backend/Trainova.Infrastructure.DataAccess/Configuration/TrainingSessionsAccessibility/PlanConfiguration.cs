using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.TrainingSessionsAccessibility
{
    public class PlanConfiguration
        : BaseEntityConfiguration<Plan>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<Plan> builder,
            bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("Plans");

            builder
                .HasOne(p => p.AccessPolicy)
                .WithMany()
                .HasForeignKey(p => p.AccessPolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.AccessPolicyId);
        }
    }

}
