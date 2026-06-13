using MediatR;
using Trainova.Application.Common.Cacheing;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.Exercises.Queries.GetExercises
{
    public record GetExercisesQuery
         : IRequest<ResultOf<IEnumerable<FitnessExerciseReadModel>>>,
        ICacheableQuery
    {
        [CacheKeyParameter]
        public ExerciseCatagory? ExerciseCatagory { get; init; }
        [CacheKeyParameter]
        public string? Search { get; init; }
        [CacheKeyParameter]
        public int Page { get; init; }
        [CacheKeyParameter]
        public int PageSize { get; init; }
        [CacheKeyParameter]
        public string? SortBy { get; init; }
        public string? SortDir { get; init; }

        public GetExercisesQuery(ExerciseCatagory? exerciseCatagory, string? search, int page, int pageSize, string? sortBy, string sortDir)
        {
            ExerciseCatagory = exerciseCatagory;
            Search = search;
            Page = page;
            PageSize = pageSize;
            SortBy = sortBy ?? "CreatedAt";
            SortDir = sortDir ?? "ASC";
        }
        public string? CacheKeyPrefix => null;

        public TimeSpan? Expiration => null;
    }
}
