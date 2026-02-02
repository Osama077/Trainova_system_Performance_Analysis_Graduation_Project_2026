using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Trainova.Domain.Events;

namespace Trainova.Domain.ComingEventPredictions
{
    public class EventPrediction
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public Event Event { get; private set; }
        public Prediction Prediction { get; private set; }
    }
    [JsonSerializable(typeof(Prediction))]
    [Owned]
    public class Prediction
    {
    }
}
