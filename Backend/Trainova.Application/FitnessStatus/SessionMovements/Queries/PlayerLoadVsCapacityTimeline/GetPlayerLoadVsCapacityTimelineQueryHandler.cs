using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.SessionMovements.Queries.PlayerLoadVsCapacityTimeline
{
    public class GetPlayerLoadVsCapacityTimelineQueryHandler(IDbSettings _dbSettings)
    : IRequestHandler<GetPlayerLoadVsCapacityTimelineQuery, ResultOf<IEnumerable<PlayerLoadVsCapacityTimelineReadModel>>>
    {
        public async Task<ResultOf<IEnumerable<PlayerLoadVsCapacityTimelineReadModel>>> Handle(
            GetPlayerLoadVsCapacityTimelineQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "[FitnessData].[sp_GetPlayerLoadVsCapacityTimeline]";

                var parameters = new
                {
                    request.PlayerId,
                    request.FromDate,
                    request.ToDate
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryAsync<PlayerLoadVsCapacityTimelineReadModel>(
                    sql, parameters, commandType: CommandType.StoredProcedure);

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPlayerLoadVsCapacityTimelineQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }

}
