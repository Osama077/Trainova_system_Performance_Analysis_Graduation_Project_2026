using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetPlayerAdheremce
{
    public class GetPlayerAdherenceQueryHandler(IDbSettings _dbSettings)
            : IRequestHandler<GetPlayerAdherenceQuery, ResultOf<IEnumerable<PlayerMonthAdherence>>>
    {
        public async Task<ResultOf<IEnumerable<PlayerMonthAdherence>>> Handle(
            GetPlayerAdherenceQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "tsa.sp_GetPlayerAdherenceOverTime";

                var parameters = new
                {
                    PlayerId = request.PlayerId
                };

                using var connection = _dbSettings.CreateReadingConnection();

                // Get adherence calculation from stored procedure
                var result = await connection.QueryAsync<PlayerMonthAdherence>(
                    sql,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPlayerAdherenceQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
