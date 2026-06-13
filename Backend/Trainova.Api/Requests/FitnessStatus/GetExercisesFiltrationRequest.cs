using Trainova.Api.Models;
using Trainova.Application.FitnessStatus.Exercises.Queries.GetExercises;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class GetExercisesFiltrationRequest : Paginator
    {

        public ExerciseCatagory? ExerciseCatagory { get; set; } = null;
        public string? Search { get; set; } = null;

        public string? SortBy { get; set; } = null;
        public string? SortDir { get; set; } = null;

        public GetExercisesQuery ToQuery()
        {
            return new GetExercisesQuery(ExerciseCatagory, Search, Page, PageSize, SortBy, SortDir);
        }

    }
}
