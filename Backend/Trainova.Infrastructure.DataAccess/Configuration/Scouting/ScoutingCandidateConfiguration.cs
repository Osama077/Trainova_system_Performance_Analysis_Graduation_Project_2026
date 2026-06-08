﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Scouting;
using Trainova.Domain.Scouting.ValueObjects;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Scouting
{
    public class ScoutingCandidateConfiguration
        : BaseEntityConfiguration<ScoutingCandidate>
    {
        protected override void ConfigureEntity(
            EntityTypeBuilder<ScoutingCandidate> builder,
            bool valueGeneratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGeneratedOnAdd);

            builder.ToTable("ScoutingCandidates");

            // Map Status enum as integer column
            builder.Property(sc => sc.Status)
                .HasConversion<int>()
                .HasColumnName("Status")
                .IsRequired()
                .HasDefaultValue(CandidateStatus.None);

            // Ensure reasonable length for FullName
            builder.Property(sc => sc.FullName).HasMaxLength(200).IsRequired();

            // Map CurrentTeamName as string
            builder.Property(sc => sc.CurrentTeamName)
                .HasColumnName("CurrentTeamName")
                .HasMaxLength(200)
                .IsRequired(false);

            // Configure owned entity: PersonalDetails
            builder.OwnsOne(sc => sc.PersonalDetails, pd =>
            {
                pd.Property(p => p.DateOfBirth)
                    .HasColumnName("DateOfBirth")
                    .IsRequired(false);
                
                pd.Property(p => p.Height)
                    .HasColumnName("Height")
                    .IsRequired(false);
                
                pd.Property(p => p.Weight)
                    .HasColumnName("Weight")
                    .IsRequired(false);
                
                pd.Property(p => p.PreferredFoot)
                    .HasColumnName("PreferredFoot")
                    .HasMaxLength(10)
                    .IsRequired(false);
            });

            // Configure owned entity: SkillAssessment
            builder.OwnsOne(sc => sc.SkillAssessment, sa =>
            {
                sa.Property(s => s.Pace)
                    .HasColumnName("Pace")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Shooting)
                    .HasColumnName("Shooting")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Dribbling)
                    .HasColumnName("Dribbling")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Passing)
                    .HasColumnName("Passing")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Physicality)
                    .HasColumnName("Physicality")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Positioning)
                    .HasColumnName("Positioning")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Defending)
                    .HasColumnName("Defending")
                    .IsRequired()
                    .HasDefaultValue(0);
                
                sa.Property(s => s.Vision)
                    .HasColumnName("Vision")
                    .IsRequired()
                    .HasDefaultValue(0);
            });

            // Configure owned entity: ContractInfo
            builder.OwnsOne(sc => sc.ContractInfo, ci =>
            {
                ci.Property(c => c.Nationality)
                    .HasColumnName("Nationality")
                    .HasMaxLength(100)
                    .IsRequired(false);
                
                ci.Property(c => c.ContractEnd)
                    .HasColumnName("ContractEnd")
                    .IsRequired(false);
                
                ci.Property(c => c.MarketValue)
                    .HasColumnName("MarketValue")
                    .HasPrecision(18, 2)
                    .IsRequired(false);
                
                ci.Property(c => c.Agent)
                    .HasColumnName("Agent")
                    .HasMaxLength(400)
                    .IsRequired(false);
            });

            builder.HasIndex(sc => sc.CurrentTeamName);

            // Map NotesList as a separate table with FK to ScoutingCandidates
            builder.HasMany(sc => sc.NotesList)
                .WithOne()
                .HasForeignKey("ScoutingCandidateId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
