using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateNote
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record DeleteCandidateNoteCommand(Guid CandidateId, Guid NoteId) : IRequest<ResultOf<bool>>;
}
