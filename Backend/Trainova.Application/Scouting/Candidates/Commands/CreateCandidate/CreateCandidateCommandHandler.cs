using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Scouting;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MedicalStatus;
using System.Threading;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Scouting.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, ResultOf<Guid>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser? _currentUser;

        public CreateCandidateCommandHandler(
            ICandidateRepository candidateRepository, 
            IUnitOfWork unitOfWork,
            CurrentUser? currentUser = null)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<ResultOf<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            // Use current user's name as the agent, or fallback to the provided agent
            var agent = _currentUser?.Name ?? request.Agent;

            var candidate = new ScoutingCandidate(
                request.FullName,
                request.Age,
                (Position)request.Position,
                request.CurrentTeamName,
                request.Nationality,
                request.ContractEnd,
                request.MarketValue,
                agent,
                request.ScoutRating,
                request.ShortlistRank,
                request.MatchesWatchedCount,
                request.Pace,
                request.Shooting,
                request.Dribbling,
                request.Passing,
                request.Physicality,
                request.Positioning,
                request.Defending,
                request.Vision,
                null,
                request.DateOfBirth,
                request.Height,
                request.Weight,
                request.PreferredFoot,
                _currentUser?.Id);

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
