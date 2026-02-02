using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.VideoInsights;

namespace Trainova.Infrastructure.DataAccess.Configuration.VideoInsights
{
    public class VideoInsightConfiguration : IEntityTypeConfiguration<VideoInsight>
    {
        public void Configure(EntityTypeBuilder<VideoInsight> builder)
        {
            builder.ToTable("VideoInsights");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.InsightType).HasMaxLength(200);
            builder.Property(v => v.Description).HasMaxLength(1000);
            builder.Property(v => v.CreatedAt).IsRequired();

            builder.HasOne(v => v.RelatedMatchVideo)
                .WithMany()
                .HasForeignKey(v => v.MatchVideoId)
                .IsRequired(false);

            // Details is an [Owned] record - EF will map owned types automatically
        }
    }
}
