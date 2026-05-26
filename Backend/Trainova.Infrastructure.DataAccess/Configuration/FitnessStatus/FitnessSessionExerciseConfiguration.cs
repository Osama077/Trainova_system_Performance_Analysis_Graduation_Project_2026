using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.FitnessStatus;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.FitnessStatus
{
    public class FitnessSessionExerciseConfiguration
        : BaseEntityConfiguration<FitnessSessionExercise>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<FitnessSessionExercise> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("FitnessSessionExercises");

            builder.HasKey(fse => fse.Id);

            builder.HasOne(fse => fse.Session)
                .WithMany()
                .HasForeignKey(fse => fse.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fse => fse.Exercise)
                .WithMany(fe => fe.SessionExercises)
                .HasForeignKey(fse => fse.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(fse => fse.LoadDetails)
                .HasMaxLength(NameLength)
                .IsRequired(false);

            builder.Property(fse => fse.Intensity)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}
