using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Queries.GetTrainingSessions;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.Profiles.Queries.GetPlayerMedicalScreen
{
    [Authorize(Roles = "Doctor,Player")]
    public record GetPlayerMedicalScreenQuery : IRequest<ResultOf<PlayerMedicalScreenDetails>>, IPlayerAuthraizedRequest
    {
        public Guid? PlayerId { get; private set; }
        public GetPlayerMedicalScreenQuery(Guid? playerId)
        {
            PlayerId = playerId;
        }
        Guid? IPlayerAuthraizedRequest.PlayerId { get => PlayerId; set => PlayerId = value.Value; }
    }
    public class PlayerMedicalScreenDetails
    {
        public List<InjuryByBart> Injuries { get; set; } = new List<InjuryByBart>();
        public List<PlayerInjury> PastInjuries { get; set; } = new List<PlayerInjury>();
        public List<TrainingSession> NextCheckup { get; set; } = new List<TrainingSession>();
        public List<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

        public PlayerMedicalScreenDetails(List<PlayerInjury> injuries, List<TrainingSession> sessions)
        {
            var now = DateTime.UtcNow;

            // 1. Filter and group active injuries (ReturnedAt is null)
            if (injuries != null)
            {
                Injuries = injuries
                    .Where(i => i.Status != InjuryStatus.Ended)
                    .GroupBy(i => i.BodyPart)
                    .Select(g => new InjuryByBart
                    {
                        BodyPart = g.Key,
                        BartInjuries = g.ToList()
                    })
                    .ToList();

                PastInjuries = injuries
                    .Where(i => i.Status == InjuryStatus.Ended)
                    .ToList();
            }

            // 3. Filter sessions into past and upcoming checkups
            if (sessions != null)
            {
                TrainingSessions = sessions
                    .Where(s => s.HappenedAt <= now)
                    .ToList();

                NextCheckup = sessions
                    .Where(s => s.HappenedAt > now)
                    .ToList();
            }
        }

        public int PastInjuriesCount => PastInjuries.Count;
        public int TrainigSessionsCount => TrainingSessions.Count;

        public int DaysMissed
        {
            get
            {
                if (PastInjuries == null || PastInjuries.Count == 0) return 0;

                return PastInjuries.Sum(i =>
                {
                    var returnDate = i.ReturnedAt ?? i.ExpectedReturnDate ?? i.CreatedAt;

                    if (returnDate == null || i.HappendAt == null) return 0;

                    var difference = returnDate.Date - i.HappendAt.Value.Date;

                    return difference.Days > 0 ? difference.Days : 0;
                });
            }
        }
        public bool AnyActive
        {
            get
            {
                return Injuries.Select(i => i.BartInjuries).Any();
            }
        }
        public DateTime? LastCheckup
        {
            get
            {
                if (TrainingSessions == null || TrainingSessions.Count == 0) return null;

                return TrainingSessions.Max(s => s.HappenedAt);
            }
        }
    }

    public class InjuryByBart
    {
        public BodyPart BodyPart { get; set; }
        public ICollection<PlayerInjury> BartInjuries { get; set; }
    }



    public class GetPlayerMedicalScreenQueryHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        ISender _sender
        ) : IRequestHandler<GetPlayerMedicalScreenQuery, ResultOf<PlayerMedicalScreenDetails>>
    {
        public async Task<ResultOf<PlayerMedicalScreenDetails>> Handle(GetPlayerMedicalScreenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sessions = await _sender.Send(new GetTrainingSessionsQuery(null, null, request.PlayerId, false));
                var injuries = await _playerInjuryRepository.GetAllAsync(playerId: request.PlayerId);

                if (sessions.IsFailure)
                    return sessions.Errors;

                return new PlayerMedicalScreenDetails(injuries.ToList(), sessions.Value.ToList());

            }
            catch (Exception ex)
            {
                return Error.Unexpected("GetPlayerMedicalScreenQueryHandler.Unexpected", ex.Message);
            }

        }
    }
}
