using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Profiles;

namespace Trainova.Application.Profiles.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandHandler(
        ICandidateRepository candidateRepository,
        IUnitOfWork unitOfWork,
        CurrentUser currentUser)
        : IRequestHandler<CreateCandidateCommand, ResultOf<Candidate>>
    {
        public async Task<ResultOf<Candidate>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var candidate = new Candidate(Guid.NewGuid(), request.FullName, request.ScoutedAt, request.Email, currentUser?.Id);

                await unitOfWork.StartTransactionAsync();

                await candidateRepository.AddAsync(candidate);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync();

                return candidate.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("CreateCandidate.Unexpected", ex.Message);
            }
        }
    }
}
