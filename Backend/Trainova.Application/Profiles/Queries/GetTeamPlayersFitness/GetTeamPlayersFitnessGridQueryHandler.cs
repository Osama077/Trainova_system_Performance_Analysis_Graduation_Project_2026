using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Queries.GetTeamPlayersFitness
{
    public class GetTeamPlayersFitnessGridQueryHandler(IDbSettings _dbSettings)
    : IRequestHandler<GetTeamPlayersFitnessGridQuery, ResultOf<IEnumerable<TeamPlayersFitnessResponse>>>
    {
        public async Task<ResultOf<IEnumerable<TeamPlayersFitnessResponse>>> Handle(
            GetTeamPlayersFitnessGridQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "[FitnessData].[sp_GetTeamPlayersFitnessGrid]";

                var parameters = new
                {
                    request.SearchName,
                    request.Position,
                    request.FootageStatus
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryAsync<PlayerFitnessGridRowReadModel>(
                    sql, parameters, commandType: CommandType.StoredProcedure);

                var groupedResult = result
                    .GroupBy(r => r.MainPosition)
                    .Select(g => new TeamPlayersFitnessResponse
                    {
                        FilteredPosition = g.Key,
                        PlayersFitnessData = g.ToList()
                    });


                return groupedResult.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetTeamPlayersFitnessGridQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }

}
