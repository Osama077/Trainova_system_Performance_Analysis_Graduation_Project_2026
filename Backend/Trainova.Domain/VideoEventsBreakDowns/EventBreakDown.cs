using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Trainova.Domain.Events;
using Trainova.Domain.Players;

namespace Trainova.Domain.VideoEventsBreakDowns
{
    public class EventBreakDown
    {
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public Guid EventId { get; private set; }
        public Event Event { get; private set; }
        public MicroEventType EventType { get; private set; }
        public MicroEventContent Content { get; private set; }

    }
    [JsonSerializable(typeof(MicroEventContent))]
    [Owned]
    public record class MicroEventContent
    {
        public string Data { get; set; }
    }
}
