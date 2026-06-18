using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Queries.GetSquadHealthProfiles
{
    public class GetSquadHealthProfilesQueryHandler(IPlayerRepository _playerRepository)
        : IRequestHandler<GetSquadHealthProfilesQuery, ResultOf<SquadHealthDetailes>>
    {
        public async Task<ResultOf<SquadHealthDetailes>> Handle(GetSquadHealthProfilesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _playerRepository.GetSquadHealthProfiles(
                    request.Position,
                    request.InjuryStatus,
                    request.SeverityGrade,
                    request.SearchName);

                if (!result.Any())
                    return new SquadHealthDetailes(result).AsZeroCount();


                return new SquadHealthDetailes(result).AsPartial();

            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetSquadHealthProfilesQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
