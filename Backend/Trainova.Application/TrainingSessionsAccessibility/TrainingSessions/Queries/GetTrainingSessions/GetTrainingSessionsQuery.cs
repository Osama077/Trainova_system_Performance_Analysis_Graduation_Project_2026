using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Queries.GetTrainingSessions
{
    [Authorize]
    public record GetTrainingSessionsQuery
        : IRequest<ResultOf<IEnumerable<TrainingSession>>>,
        IPlayerAuthraizedRequest,
        ICreatorAuthraizedRequest
    {
        public bool IncludeCreateror { get; init; }
        public Guid? PlayerId { get; private set; } = null;
        public DateTime From { get; init; }
        public DateTime To { get; init; }
        public Guid? CreatorId { get; private set; } = null;

        Guid? IPlayerAuthraizedRequest.PlayerId 
        {
            get => PlayerId;
            set => PlayerId = value;
        }
        Guid? ICreatorAuthraizedRequest.CreatorId 
        {
            get => CreatorId;
            set => CreatorId = value;
        }

        public GetTrainingSessionsQuery(DateTime from, DateTime to, Guid? playerId, bool includeCreateror)
        {
            From = from;
            To = to;
            PlayerId = playerId;
            IncludeCreateror = includeCreateror;
        }

    }
}
