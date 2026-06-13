using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Cacheing;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Queries.GetExercisesBySessionId
{
    [Authorize]
    public record GetFitnessSessionExercisesQuery
        : IRequest<ResultOf<IEnumerable<FitnessSessionExercisesReadModel>>>,
        ICacheableQuery
    {
        [CacheKeyParameter]
        public Guid? SessionId { get; init; }
        [CacheKeyParameter]
        public Guid? ExerciseId { get; init; }
        public GetFitnessSessionExercisesQuery(Guid? sessionId = null, Guid? exerciseId = null)
        {
            SessionId = sessionId;
            ExerciseId = exerciseId;
        }
        public string CacheKeyPrefix => null;

        public TimeSpan? Expiration
        {
            get
            {
                if (SessionId.HasValue && !ExerciseId.HasValue)
                    return TimeSpan.FromHours(1);
                else if (!SessionId.HasValue && ExerciseId.HasValue)
                    return TimeSpan.FromHours(6);
                else
                    return null;
            }
        }
    }
    public class GetFitnessSessionExercisesBySessionIdQueryHandler(IDbSettings _dbSettings)
        : IRequestHandler<GetFitnessSessionExercisesQuery, ResultOf<IEnumerable<FitnessSessionExercisesReadModel>>>
    {
        public async Task<ResultOf<IEnumerable<FitnessSessionExercisesReadModel>>> Handle(
            GetFitnessSessionExercisesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "FitnessData.sp_GetFitnessSessionExercises";

                var paramters = new
                {
                    SessionId = request.SessionId,
                    ExerciseId = request.ExerciseId,
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var result = await connection.QueryAsync<FitnessSessionExercisesReadModel>(
                    sql, paramters, commandType: CommandType.StoredProcedure);

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetFitnessSessionExercisesBySessionIdQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
