using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityTests
{
    public class GetPhysicalCapacityTestsQueryHandler(IDbSettings _dbSettings)
    : IRequestHandler<GetPhysicalCapacityTestsQuery, ResultOf<IEnumerable<PlayerPhysicalCapacityTestReadModel>>>
    {
        public async Task<ResultOf<IEnumerable<PlayerPhysicalCapacityTestReadModel>>> Handle(
            GetPhysicalCapacityTestsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "[FitnessData].[sp_GetRecentPhysicalCapacityTests]";

                var parameters = new
                {
                    request.PlayerId,
                    request.SearchName,
                    request.FromDate,
                    request.ToDate
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryAsync<PlayerPhysicalCapacityTestReadModel>(
                    sql, parameters, commandType: CommandType.StoredProcedure);

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPhysicalCapacityTestsQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }



}
