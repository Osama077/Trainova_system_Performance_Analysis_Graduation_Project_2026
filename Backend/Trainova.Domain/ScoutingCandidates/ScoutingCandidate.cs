using Trainova.Domain.Common;

namespace Trainova.Domain.ScoutingCandidates
{
    public class ScoutingCandidate
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public Position Position { get; private set; }
        public float PerformanceScore { get; private set; }
        public float InjuryRisk { get; private set; }
    }
}
