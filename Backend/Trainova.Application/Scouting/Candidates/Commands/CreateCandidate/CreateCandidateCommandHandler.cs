using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Scouting;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MedicalStatus;
using System.Threading;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, ResultOf<Guid>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
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
                // Return a structured error instead of throwing to allow ApiController to map it
                return Error.Failure("CreateCandidate.Failed", $"Failed to create scouting candidate: {ex.Message}").AsError<Guid>();
            }

            return candidate.Id.AsCreated();
        }
    }
}
