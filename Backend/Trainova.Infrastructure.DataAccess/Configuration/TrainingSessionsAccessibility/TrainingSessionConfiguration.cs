using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.TrainingSessionsAccessibility;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.TrainingSessionsAccessibility
{
    public class TrainingSessionConfiguration : BaseEntityConfiguration<TrainingSession>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<TrainingSession> builder, bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("TrainingSessions");

            builder.Property(ts => ts.SessionType)
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.HasKey(t => t.Id);
            builder.HasOne(ts=>ts.AccessPolicy)
                .WithMany()
                .HasForeignKey(ts => ts.AccessPolicyId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
