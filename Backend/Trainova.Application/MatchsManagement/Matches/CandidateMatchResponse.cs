namespace Trainova.Application.MatchsManagement.Matches
{
    public class CandidateMatchResponse
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime MatchDate { get; set; }
        public string OpponentName { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string? Notes { get; set; }
    }
}
