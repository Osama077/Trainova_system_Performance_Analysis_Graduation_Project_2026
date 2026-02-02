using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Trainova.Domain.Videos;

namespace Trainova.Domain.VideoInsights
{
    public class VideoInsight
    {
        public Guid Id { get; private set; }
        public Guid? MatchVideoId { get; private set; }
        public MatchVideo? RelatedMatchVideo { get; private set; }

        public Insight Details { get; private set; }

        public string? InsightType { get; private set; }
        public string? Description { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
    [JsonSerializable(typeof(Insight))]
    [Owned]
    public record Insight
    {
        // instance properties ??
    }

}
