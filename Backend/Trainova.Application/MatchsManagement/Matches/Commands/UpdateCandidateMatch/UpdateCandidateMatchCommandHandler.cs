using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Application.MatchsManagement.Matches.Commands.UpdateCandidateMatch
{
    public class UpdateCandidateMatchCommandHandler(
        ICandidateMatchRepository candidateMatchRepository,
        ICandidateRepository candidateRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateCandidateMatchCommand, ResultOf<CandidateMatch>>
    {
        public async Task<ResultOf<CandidateMatch>> Handle(UpdateCandidateMatchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var match = await candidateMatchRepository.GetByIdAsync(request.Id);
                if (match is null)
                    return Error.NotFound("UpdateCandidateMatch.MatchNotFound", "Candidate match not found");

                if (request.CandidateId.HasValue && request.CandidateId.Value != match.CandidateId)
                {
                    var candidate = await candidateRepository.GetByIdAsync(request.CandidateId.Value);
                    if (candidate is null)
                        return Error.NotFound("UpdateCandidateMatch.CandidateNotFound", "Candidate not found");
                }

                match.Update(
                    matchDate: request.MatchDate,
                    opponentName: request.OpponentName,
                    homeScore: request.HomeScore,
                    awayScore: request.AwayScore,
                    notes: request.Notes);

                await unitOfWork.StartTransactionAsync();

                await candidateMatchRepository.UpdateAsync(match);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync();

                return match.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("UpdateCandidateMatch.Unexpected", ex.Message);
            }
        }
    }
}
