using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Queries.GetTrainingSessions
{
    public class GetTrainingSessionsQueryHandler(
        ITrainingSessionRepository _trainingSessionRepository)
        : IRequestHandler<GetTrainingSessionsQuery, ResultOf<IEnumerable<TrainingSession>>>
    {
        public async Task<ResultOf<IEnumerable<TrainingSession>>> Handle(GetTrainingSessionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sessions = await _trainingSessionRepository.GetTrainingSessionsAsync(
                    from: request.From,
                    to: request.To,
                    userAccsessPolicyId: request.PlayerId,
                    creatorId: request.CreatorId
                    );

                return sessions.AsPartial();

            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetTrainingSessionsQueryHandler.Handle_Failure",
                    description: ex.Message
                    );
            }
        }
    }
}
