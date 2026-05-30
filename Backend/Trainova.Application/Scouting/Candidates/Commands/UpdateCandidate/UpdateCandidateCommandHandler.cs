using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System.Threading;

namespace Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, string?>
    {
        private readonly ICandidateRepository _candidateRepository;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<string?> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (candidate == null) return null;

            candidate.Update(
                request.FullName,
                request.Age,
                null,
                request.CurrentMainPosition.HasValue ? (Position?)request.CurrentMainPosition.Value : null,
                request.OtherAvailablePositions.HasValue ? (Position?)request.OtherAvailablePositions.Value : null,
                request.PerformanceLevel,
                null, // nationality
                null, // contractEnd
                null, // marketValue
                null, // agent
                null, // scoutRating
                null, // shortlistRank
                null, // matchesWatchedCount
                null, // pace
                null, // shooting
                null, // dribbling
                null, // passing
                null, // physicality
                null, // positioning
                null, // defending
                null, // vision
                request.Note);

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);

                // Readback to verify persistence
                var refreshed = await _candidateRepository.GetByIdAsync(request.Id, cancellationToken);
                var savedNotes = refreshed?.Notes;

                // quick console log to inspect value during local debugging
                System.Console.WriteLine($"[DEBUG] Saved notes for candidate {request.Id}: {savedNotes}");

                return savedNotes;
            }
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException($"Failed to update candidate {request.Id}: {ex.Message}", ex);
            }
        }
    }
}
