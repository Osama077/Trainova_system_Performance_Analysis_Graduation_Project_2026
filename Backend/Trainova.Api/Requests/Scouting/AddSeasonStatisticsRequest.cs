using System;

namespace Trainova.Api.Requests.Scouting
{
    public class AddSeasonStatisticsRequest
    {
        public string Season { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Matches { get; set; }
        public float PassAccuracy { get; set; }
        public float ShotsPer90 { get; set; }
        public float XgPer90 { get; set; }
    }
}
