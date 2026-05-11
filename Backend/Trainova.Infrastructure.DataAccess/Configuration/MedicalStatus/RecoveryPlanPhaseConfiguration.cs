using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MedicalStatus;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class RecoveryPlanPhaseConfiguration : BaseEntityConfiguration<RecoveryPlanPhase>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RecoveryPlanPhase> builder, bool valueGeneratedOnAdd = true)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.HasIndex(p => new { p.PlayerInjuryId, p.Order });


        }
    }
}
