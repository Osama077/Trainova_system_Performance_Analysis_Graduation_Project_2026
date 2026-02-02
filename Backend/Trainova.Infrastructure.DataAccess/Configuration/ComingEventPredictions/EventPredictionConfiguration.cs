using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainova.Domain.ComingEventPredictions;

namespace Trainova.Infrastructure.DataAccess.Configuration.ComingEventPredictions
{
    public class EventPredictionConfiguration : IEntityTypeConfiguration<EventPrediction>
    {
        public void Configure(EntityTypeBuilder<EventPrediction> builder)
        {
            builder.ToTable("EventPredictions");
            builder.HasKey(ep => ep.Id);

            builder.HasOne(ep => ep.Event)
                .WithMany()
                .HasForeignKey(ep => ep.EventId);

            // Prediction is [Owned]
        }
    }
}
