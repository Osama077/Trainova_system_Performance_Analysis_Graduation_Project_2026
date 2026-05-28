using Trainova.Application.Common.Interfaces.MarkUps;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases
{
    public class PlayerInjuryRecoveryPlanData : ITotalCountIncluded
    {
        public DateTime? StartFrom { get; init; }
        public DateTime? EndAt { get; init; }
        public decimal TotalProgress { get; init; }
        public IEnumerable<RecoveryPlanPhaseDetailes> Phases { get; init; }

        public int TotalCount { get; init; }

        public PlayerInjuryRecoveryPlanData(PlayerInjury injuryCase)
        {
            StartFrom = injuryCase.HappendAt;
            EndAt = injuryCase.ReturnedAt ?? injuryCase.ExpectedReturnDate;
            Phases = RecoveryPlanPhaseDetailes.MapFrom(injuryCase.Phases);
            TotalProgress = Phases.Any() ? Phases.Average(p => p.Progress) : 0;
            TotalProgress = injuryCase.Status == InjuryStatus.Ended
                ? 100
                : (decimal)(DateTime.UtcNow - StartFrom.Value).TotalDays / (decimal)(EndAt.Value - StartFrom.Value).TotalDays * 100;
        }

        public class RecoveryPlanPhaseDetailes
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public int Order { get; set; }
            public List<string> Activities { get; set; } = null!;

            public DateTime CreatedAt { get; set; }
            public DateTime LastUpdate { get; set; }

            public Guid CreatedBy { get; set; }
            public decimal Progress { get; set; }
            public static IEnumerable<RecoveryPlanPhaseDetailes> MapFrom(IEnumerable<RecoveryPlanPhase> phases)
            {
                return phases.Select(p => new RecoveryPlanPhaseDetailes
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    From = p.From,
                    To = p.To,
                    Order = p.Order,
                    Activities = p.Activities,
                    CreatedAt = p.CreatedAt,
                    LastUpdate = p.LastUpdate ?? p.CreatedAt,
                    CreatedBy = p.CreatedBy.HasValue ? p.CreatedBy.Value : Guid.Empty,
                    Progress = p.From <= DateTime.UtcNow && p.To >= DateTime.UtcNow
                    ? (decimal)(DateTime.UtcNow - p.From).TotalDays / (decimal)(p.To - p.From).TotalDays * 100
                    : (p.From > DateTime.UtcNow ? 0 : 100)
                });
            }

        }
    }

}
