using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.FitnessStatus.PhysicalCapacityTests;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.FitnessStatus
{
    public class PhysicalCapacityTestConfiguration
        : BaseEntityConfiguration<PhysicalCapacityTest>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<PhysicalCapacityTest> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("CapacityTests");

            builder.HasKey(pct => pct.Id);

            builder.Property(pct => pct.PlayerId)
                .IsRequired();

            builder.OwnsOne(pct => pct.AerobicCapacityTest, aerobic =>
            {
                aerobic.Property(a => a.MaximumOxygenConsumption)
                    .HasColumnName("MaximumOxygenConsumption")
                    .HasPrecision(10, 2);

                aerobic.Property(a => a.YoYoIntermittentRecoveryLevel1Distance)
                    .HasColumnName("YoYoIntermittentRecoveryLevel1Distance");

                aerobic.Property(a => a.YoYoIntermittentRecoveryLevel2Distance)
                    .HasColumnName("YoYoIntermittentRecoveryLevel2Distance");
            });

            builder.OwnsOne(pct => pct.SprintTest, sprint =>
            {
                sprint.Property(s => s.Time10Meters)
                    .HasColumnName("Time10Meters")
                    .HasPrecision(10, 2);

                sprint.Property(s => s.Time30Meters)
                    .HasColumnName("Time30Meters")
                    .HasPrecision(10, 2);
            });

            builder.OwnsOne(pct => pct.ExplosivePowerTest, explosive =>
            {
                explosive.Property(e => e.CountermovementJumpHeight)
                    .HasColumnName("CountermovementJumpHeight")
                    .HasPrecision(10, 2);

                explosive.Property(e => e.ReactiveStrengthIndex)
                    .HasColumnName("ReactiveStrengthIndex")
                    .HasPrecision(10, 2);
            });
        }
    }
}
