using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidate
{
    public record DeleteCandidateCommand(Guid CandidateId) : IRequest<ResultOf<bool>>;
}
