using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.MatchsManagement.Matches
{
    public class CandidateMatchConfiguration : BaseEntityConfiguration<CandidateMatch>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CandidateMatch> builder, bool valueGenratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGenratedOnAdd);

            builder.Property(cm => cm.CandidateId).IsRequired();
            builder.Property(cm => cm.MatchDate).HasColumnType("datetime2").IsRequired();
            builder.Property(cm => cm.OpponentName).HasMaxLength(200).IsRequired();
            builder.Property(cm => cm.Notes).HasMaxLength(2000);

            builder.ToTable("CandidateMatches");
        }
    }
}
