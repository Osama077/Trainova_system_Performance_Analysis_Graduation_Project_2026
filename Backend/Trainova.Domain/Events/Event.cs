using System.Diagnostics;
using Trainova.Domain.ComingEventPredictions;
using Trainova.Domain.Common;
using Trainova.Domain.Plans;
using Trainova.Domain.VideoEventsBreakDowns;
using Trainova.Domain.Videos;

namespace Trainova.Domain.Events
{
    public class Event : IAuditable
    {
        public Guid Id { get; private set; }
        public string EventName { get; private set; }
        public Guid? PlanId { get; private set; }
        public Plan? Plan { get; private set; }
        public string? Place { get; private set; }
        public DateTime? HappenedAt { get; private set; }
        public ManageableBy ManageableBy { get; private set; }= ManageableBy.Owner;

        public ICollection<EventPrediction> Predictions { get; private set; } = [];
        public ICollection<MatchVideo> MatchVideos { get; private set; } = [];
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }

    }

}
