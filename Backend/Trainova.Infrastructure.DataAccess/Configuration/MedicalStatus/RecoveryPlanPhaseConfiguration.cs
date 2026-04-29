using Microsoft.EntityFrameworkCore;
using Trainova.Domain.MedicalStatus.RecoveryPlans;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class RecoveryPlanPhaseConfiguration : BaseEntityConfiguration<RecoveryPlanPhase>
    {
        override protected void ConfigureEntity(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RecoveryPlanPhase> builder, bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);
            builder.ToTable("RecoveryPlanPhases");
            builder
                .HasOne(rpp => rpp.PlayerInjury)
                .WithOne()
                .HasForeignKey<RecoveryPlanPhase>(rpp => rpp.PlayerInjuryId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder
                .Property(rpp => rpp.Activities)
                .HasConversion(
                    activities => string.Join(";", activities),
                    dbValue => dbValue.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}
