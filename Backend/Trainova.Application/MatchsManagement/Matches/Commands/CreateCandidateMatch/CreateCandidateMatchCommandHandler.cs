using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Application.MatchsManagement.Matches.Commands.CreateCandidateMatch
{
    public class CreateCandidateMatchCommandHandler(
        ICandidateMatchRepository candidateMatchRepository,
        ICandidateRepository candidateRepository,
        IUnitOfWork unitOfWork,
        CurrentUser currentUser)
        : IRequestHandler<CreateCandidateMatchCommand, ResultOf<CandidateMatch>>
    {
        public async Task<ResultOf<CandidateMatch>> Handle(CreateCandidateMatchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var candidate = await candidateRepository.GetByIdAsync(request.CandidateId);
                if (candidate is null)
                    return Error.NotFound("CreateCandidateMatch.CandidateNotFound", "Candidate not found");

                var entity = new CandidateMatch(
                    Guid.NewGuid(),
                    request.CandidateId,
                    request.MatchDate,
                    request.OpponentName,
                    request.HomeScore,
                    request.AwayScore,
                    request.Notes,
                    currentUser?.Id);

                await unitOfWork.StartTransactionAsync();

                await candidateMatchRepository.AddAsync(entity);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync();

                return entity.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("CreateCandidateMatch.Unexpected", ex.Message);
            }
        }
    }
}
