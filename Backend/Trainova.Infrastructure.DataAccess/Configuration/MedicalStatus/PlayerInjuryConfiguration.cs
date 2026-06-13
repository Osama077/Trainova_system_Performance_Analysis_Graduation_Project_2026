using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Trainova.Domain.MedicalStatus;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MedicalStatus
{
    public class PlayerInjuryConfiguration
        : BaseEntityConfiguration<PlayerInjury>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<PlayerInjury> builder,
            bool valueGeneratedOnAdd = false)
        {
            // Always call base first
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            //----------------------------------------
            // Table
            //----------------------------------------

            builder.ToTable("PlayerInjuries");

            //----------------------------------------
            // Relationships
            //----------------------------------------

            builder
                .HasOne(pi => pi.Player)
                .WithMany(p => p.PlayerInjuries)
                .HasForeignKey(pi => pi.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pi => pi.Injury)
                .WithMany(i => i.PlayerInjuries)
                .HasForeignKey(pi => pi.InjuryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsMany(p => p.Phases, phaseBuilder =>
            {
                phaseBuilder.WithOwner().HasForeignKey(pp => pp.PlayerInjuryId);

                phaseBuilder.ToTable("RecoveryPlanPhases");

                phaseBuilder.HasKey(pp => pp.Id);

                phaseBuilder.HasIndex(pp => new { pp.PlayerInjuryId, pp.Order });
                phaseBuilder.Ignore(p => p.IsAdded);
                phaseBuilder.Property(pp => pp.Activities)
                    .HasConversion(
                        value => JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
                        value => JsonSerializer.Deserialize<List<string>>(value, JsonSerializerOptions.Default) ?? new List<string>()
                    );
            });


            //----------------------------------------
            // Enums
            //----------------------------------------

            builder
                .Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder
                .Property(p => p.Cause)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(p => p.BodyPart)
                .HasConversion<string>()
                .HasMaxLength(400);
            //----------------------------------------
            // Defaults
            //----------------------------------------

            builder
                .Property(p => p.IsNew)
                .HasDefaultValue(false);

            //----------------------------------------
            // Indexes (useful for queries)
            //----------------------------------------

            builder
                .HasIndex(p => p.PlayerId);

            builder
                .HasIndex(p => p.InjuryId);

            builder
                .HasIndex(p => p.Status);




        }
    }


}
