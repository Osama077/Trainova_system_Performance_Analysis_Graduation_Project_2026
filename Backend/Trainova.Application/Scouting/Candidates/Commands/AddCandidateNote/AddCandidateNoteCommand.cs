using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateNote
{
    public record AddCandidateNoteCommand(Guid CandidateId, string Text) : IRequest<ResultOf<Guid>>;
}
