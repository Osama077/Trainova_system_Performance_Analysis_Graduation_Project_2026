using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Plans;

namespace Trainova.Infrastructure.DataAccess.Configuration.Plans
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PlanName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.PlanGoul).HasMaxLength(1000);

            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate);

            builder.Property(p => p.CreatedAt).IsRequired();

            // AccessPolicy relationship
            builder.HasOne(p => p.AccessPolicy)
                .WithMany()
                .HasForeignKey(p => p.AccessPolicyId);

            // ManageableBy is a [Flags] enum - leave numeric
            builder.Property(p => p.ManageableBy);

            // PlanState enum - leave numeric unless annotated
            builder.Property(p => p.State);
        }
    }
}
