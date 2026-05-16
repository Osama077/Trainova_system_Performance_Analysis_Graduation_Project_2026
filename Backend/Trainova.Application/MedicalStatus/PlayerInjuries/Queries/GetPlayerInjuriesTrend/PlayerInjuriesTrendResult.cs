namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuriesTrend
{
    public class PlayerInjuriesTrendResult
    {
        public IEnumerable<PlayerInjuryReadModel> PlayerInjuries { get; set; }
        public int TotalPlayerInjuries { get => PlayerInjuries.Count(); }
        public IEnumerable<MonthInjury> MonthsInjuries { get; set; }
        public int MonthsInjuriesCount { get => MonthsInjuries.Count(); }

        public PlayerInjuriesTrendResult(IEnumerable<PlayerInjuryReadModel> playerInjuries)
        {
            if (!playerInjuries.Any())
            {
                PlayerInjuries = playerInjuries;
                MonthsInjuries = new List<MonthInjury>();
                return;
            }
            PlayerInjuries = playerInjuries;
            MonthsInjuries = playerInjuries
                .GroupBy(pi => pi.PlayerInjuryCreatedAt.ToString("yyyy-MM"))
                    .Select(g => new MonthInjury(g.Key, g.Count()));
        }

    }
    public record MonthInjury(string Month, int Count);

}
