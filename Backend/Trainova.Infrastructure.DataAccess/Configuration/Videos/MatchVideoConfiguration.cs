using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.Videos;

namespace Trainova.Infrastructure.DataAccess.Configuration.Videos
{
    public class MatchVideoConfiguration : IEntityTypeConfiguration<MatchVideo>
    {
        public void Configure(EntityTypeBuilder<MatchVideo> builder)
        {
            builder.ToTable("MatchVideos");
            builder.HasKey(mv => mv.Id);

            builder.Property(mv => mv.VideoUrl).IsRequired().HasMaxLength(1000);
            builder.Property(mv => mv.CreatedAt).IsRequired();

            builder.HasOne(mv => mv.RelatedEvent)
                .WithMany()
                .HasForeignKey(mv => mv.EventId)
                .IsRequired(false);

            // Insights is an owned or separate entity; leave mapping to its own config if needed
        }
    }
}
