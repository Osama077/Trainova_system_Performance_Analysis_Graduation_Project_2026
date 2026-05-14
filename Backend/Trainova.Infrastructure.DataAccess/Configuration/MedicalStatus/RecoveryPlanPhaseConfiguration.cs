using MailKit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
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

            builder.Property(pp => pp.Activities)
                .HasConversion<string>(
                value => JsonSerializer.Serialize(value),
                value => JsonSerializer.Deserialize<List<string>>(value)
                );
        }
    }
}
