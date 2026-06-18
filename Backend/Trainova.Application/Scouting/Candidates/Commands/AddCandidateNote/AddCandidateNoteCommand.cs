using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateNote
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record AddCandidateNoteCommand(Guid CandidateId, string Text) : IRequest<ResultOf<Guid>>;
}
