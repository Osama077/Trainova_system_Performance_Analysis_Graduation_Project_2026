using Trainova.Domain.Common;
using Trainova.Domain.Events;
using Trainova.Domain.VideoInsights;

namespace Trainova.Domain.Videos
{
    public class MatchVideo : IAuditable
    {
        public Guid Id { get; private set; }
        public string VideoUrl { get; private set; }
        public Guid? EventId { get; private set; }
        public Event? RelatedEvent { get; private set; }
        public ICollection<VideoInsight> Insights { get; private set; } = [];
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }

    }
}
