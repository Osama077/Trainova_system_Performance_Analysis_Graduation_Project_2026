using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPlayerFitnessMetricsSummary
{
    public class GetPlayerFitnessMetricsSummaryQueryHandler(IDbSettings _dbSettings)
    : IRequestHandler<GetPlayerFitnessMetricsSummaryQuery, ResultOf<PlayerFitnessMetricsReadModel>>
    {
        public async Task<ResultOf<PlayerFitnessMetricsReadModel>> Handle(
            GetPlayerFitnessMetricsSummaryQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "[FitnessData].[sp_GetPlayerFitnessMetricsSummary]";

                var parameters = new { request.PlayerId };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryFirstOrDefaultAsync<PlayerFitnessMetricsReadModel>(
                    sql, parameters, commandType: CommandType.StoredProcedure);

                if (result is null)
                {
                    return Error.NotFound(
                        code: "GetPlayerFitnessMetricsSummary.NotFound",
                        description: "No fitness records found for the given player context.");
                }

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPlayerFitnessMetricsSummaryQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
