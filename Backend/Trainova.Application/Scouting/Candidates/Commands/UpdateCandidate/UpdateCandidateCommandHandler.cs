using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System.Threading;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate
{
    [System.Obsolete("UpdateCandidateCommandHandler is deprecated: candidate edits are no longer supported via PUT. Use specific endpoints (status, notes) instead.")]
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, ResultOf<string?>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly CurrentUser? _currentUser;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, CurrentUser? currentUser = null)
        {
            _candidateRepository = candidateRepository;
            _currentUser = currentUser;
        }

        public async Task<ResultOf<string?>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            return Error.Failure("UpdateCandidate.Deprecated", "UpdateCandidate endpoint is deprecated. Use status or notes endpoints instead.").AsError<string?>();
        }
    }
}
