using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateNote
{
    public record DeleteCandidateNoteCommand(Guid CandidateId, Guid NoteId) : IRequest<ResultOf<bool>>;
}
