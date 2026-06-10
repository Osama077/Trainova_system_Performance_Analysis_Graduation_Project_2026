using System;

namespace Trainova.Api.Requests.Scouting
{
    public class AddCandidateMatchRequest
    {
        public DateTime MatchDate { get; set; }
        public string MatchName { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public float Rating { get; set; }
        public string? ScoutNotes { get; set; }
    }
}
