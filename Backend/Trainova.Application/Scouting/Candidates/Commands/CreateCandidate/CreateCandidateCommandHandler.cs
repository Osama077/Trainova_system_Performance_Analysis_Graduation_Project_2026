using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Scouting;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MedicalStatus;
using System.Threading;

namespace Trainova.Application.Scouting.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Guid>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new ScoutingCandidate(
                request.FullName,
                request.Age,
                (Position)request.Position,
                request.PerformanceScore,
                request.InjuryRisk,
                PlayerMedicalStatus.Fit,
                (Position)request.CurrentMainPosition,
                (Position)request.OtherAvailablePositions,
                request.PerformanceLevel,
                request.CurrentTeamId,
                null);

            try
            {
                await _candidateRepository.AddAsync(candidate, cancellationToken);
                // Persist changes so the candidate is actually saved in the database
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException($"Failed to create scouting candidate: {ex.Message}", ex);
            }

            return candidate.Id;
        }
    }
}
