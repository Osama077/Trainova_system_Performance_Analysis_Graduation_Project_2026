namespace Trainova.Domain.Scouting.ValueObjects
{
    /// <summary>
    /// Value object representing player's contract and market information
    /// </summary>
    public class ContractInfo
    {
        public string? Nationality { get; private set; }
        public DateTime? ContractEnd { get; private set; }
        public decimal? MarketValue { get; private set; }
        public string? Agent { get; private set; }

        public ContractInfo(
            string? nationality = null,
            DateTime? contractEnd = null,
            decimal? marketValue = null,
            string? agent = null)
        {
            Nationality = nationality;
            ContractEnd = contractEnd;
            MarketValue = marketValue;
            Agent = agent;
        }

        public void Update(
            string? nationality = null,
            DateTime? contractEnd = null,
            decimal? marketValue = null,
            string? agent = null)
        {
            if (!string.IsNullOrEmpty(nationality))
                Nationality = nationality;
            if (contractEnd.HasValue)
                ContractEnd = contractEnd;
            if (marketValue.HasValue)
                MarketValue = marketValue;
            if (!string.IsNullOrEmpty(agent))
                Agent = agent;
        }
    }
}
