using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetPlayerAdheremce
{
    [Authorize(Roles = "Player,FitnessCoach,HeadCoach")]
    public record GetPlayerAdherenceQuery : IRequest<ResultOf<IEnumerable<PlayerMonthAdherence>>>, IPlayerAuthraizedRequest
    {
        public GetPlayerAdherenceQuery(Guid playerId)
        {
            PlayerId = playerId;
        }
        public Guid PlayerId { get; private set; }
        Guid? IPlayerAuthraizedRequest.PlayerId
        {
            get => PlayerId;
            set => PlayerId = value.Value;
        }
    }

    public class PlayerMonthAdherence
    {
        public string Month { get; set; }
        public decimal Adherence { get; set; } = 0;
    }
}
