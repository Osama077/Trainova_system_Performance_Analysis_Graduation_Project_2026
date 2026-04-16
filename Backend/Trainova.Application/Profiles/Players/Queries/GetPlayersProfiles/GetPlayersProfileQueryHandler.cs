using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Players.Queries.GetPlayersProfiles
{
    public class GetPlayersProfileQueryHandler(
        IPlayerRepository _playerRepository)
        : IRequestHandler<GetPlayersProfileQuery, ResultOf<IEnumerable<PlayerDetailResponse>>>
    {
        public async Task<ResultOf<IEnumerable<PlayerDetailResponse>>> Handle(GetPlayersProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var players = await _playerRepository.GetPlayersAsync(
                    playerId: request.PlayerId,
                    searchTerm: request.SearchTerm,
                    teamId: request.TeamId,
                    performanceLevel: request.PerformanceLevel,
                    isActive: request.IsActive,
                    mainPositionFilter: request.MainPositionFilter,
                    otherPositionFilter: request.OtherPositionFilter,
                    dateFrom: request.DateFrom,
                    dateTo: request.DateTo,
                    minMatches: request.MinMatches,
                    medicalStatus: request.MedicalStatus,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    sortDirection: request.SortDirection,
                    sortColumn: request.SortColumn);

                if (!players.Any())
                {
                    return Error.NotFound(
                    code: "GetPlayersProfileQueryHandler.Handle_Failure",
                    description: "no profiles found for the Specific area");
                }

                return players.AsPartial();

            }
            catch(Exception ex) 
            {
                return Error.Failure(
                    code: "GetPlayersProfileQueryHandler.Handle_Failure",
                    description: ex.Message);
            }

        }
    }
}
