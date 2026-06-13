using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.Exercises.Queries.GetExercises
{
    public class GetExercisesQueryHadler(IDbSettings _dbSettings) : IRequestHandler<GetExercisesQuery, ResultOf<IEnumerable<FitnessExerciseReadModel>>>
    {

        public async Task<ResultOf<IEnumerable<FitnessExerciseReadModel>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "[FitnessData].[sp_GetExercises_FilteredAndPaged]";

                var paramters = new
                {
                    request.SortDir,
                    request.Search,
                    request.SortBy,
                    request.PageSize,
                    request.Page,
                    request.ExerciseCatagory
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryAsync<FitnessExerciseReadModel>(
                    sql, paramters, commandType: CommandType.StoredProcedure);

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetExercisesQueryHadler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
