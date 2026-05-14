namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount
{
    public class CasesCountResponse
    {
        public int CurrentlyInjuredCount { get; set; }

        public int ActiveInHealing { get; set; }
        public int ActiveInHealingIncrease { get; set; }

        public int RecoveredEnded { get; set; }
        public int RecoveredEndedIncrease { get; set; }

        public int InRecovery { get; set; }
        public int InRecoveryIncrease { get; set; }

        public int NewInjuries { get; set; }
        public int NewInjuriesIncrease { get; set; }

        public int NotNewInjuries { get; set; }
        public int NotNewInjuriesIncrease { get; set; }

        public int TotalMonitoredCases { get; set; }
        public int TotalMonitoredIncrease { get; set; }
    }
}