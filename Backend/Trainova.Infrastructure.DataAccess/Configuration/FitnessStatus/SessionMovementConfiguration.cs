using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.FitnessStatus.MovementDistances;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.FitnessStatus
{
    public class SessionMovementConfiguration
        : BaseEntityConfiguration<SessionMovement>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<SessionMovement> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            // =========================
            // Relationships
            // =========================

            builder
                .HasOne(sm => sm.Player)
                .WithMany()
                .HasForeignKey(sm => sm.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sm => sm.TrainingSession)
                .WithMany()
                .HasForeignKey(sm => sm.TrainingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Simple Properties
            // =========================

            builder
                .Property(sm => sm.SprintsCount)
                .IsRequired(false);

            builder
                .Property(sm => sm.PlayerLoad)
                .HasPrecision(10, 2)
                .IsRequired(false);

            // =========================
            // Distance (Owned)
            // =========================

            builder
                .OwnsOne(sm => sm.Distance, distance =>
                {
                    distance.Property(d => d.TotalDistance)
                            .HasColumnName("TotalDistance")
                            .HasPrecision(10, 2);

                    distance.Property(d => d.WalkDistance)
                            .HasColumnName("WalkDistance")
                            .HasPrecision(10, 2);

                    distance.Property(d => d.RunDistance)
                            .HasColumnName("RunDistance")
                            .HasPrecision(10, 2);

                    distance.Property(d => d.HighSpeedRunDistance)
                            .HasColumnName("HighSpeedRunDistance")
                            .HasPrecision(10, 2);
                });

            builder
                .Navigation(sm => sm.Distance)
                .IsRequired(false);

            // =========================
            // Speed (Owned)
            // =========================

            builder
                .OwnsOne(sm => sm.Speed, speed =>
                {
                    speed.Property(s => s.AverageSpeed)
                         .HasColumnName("AverageSpeed")
                         .HasPrecision(10, 2);

                    speed.Property(s => s.MaxSpeed)
                         .HasColumnName("MaxSpeed")
                         .HasPrecision(10, 2);

                    speed.Property(s => s.PeakAcceleration)
                         .HasColumnName("PeakAcceleration")
                         .HasPrecision(10, 2);
                });

            builder
                .Navigation(sm => sm.Speed)
                .IsRequired(false);
        }
    }

}
