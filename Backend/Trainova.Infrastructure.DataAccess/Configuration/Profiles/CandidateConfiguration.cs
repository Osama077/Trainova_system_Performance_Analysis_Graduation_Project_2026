using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Profiles;
using Trainova.Infrastructure.DataAccess.Configuration.Common;

namespace Trainova.Infrastructure.DataAccess.Configuration.Profiles
{
    public class CandidateConfiguration : BaseEntityConfiguration<Candidate>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Candidate> builder, bool valueGenratedOnAdd = false)
        {
            base.ConfigureEntity(builder, valueGenratedOnAdd);

            builder.Property(c => c.FullName).HasMaxLength(NameLength).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(256);
            builder.Property(c => c.ScoutedAt).HasColumnType("datetime2").IsRequired();
            builder.Property(c => c.IsShortlisted).HasDefaultValue(false);

            builder.ToTable("Candidates");
        }
    }
}
