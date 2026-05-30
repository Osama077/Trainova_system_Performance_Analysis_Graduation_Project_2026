using System;
using Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus;

namespace Trainova.Api.Requests.Profiles
{
    public class SetCandidateStatusRequest
    {
        public int Flags { get; set; }
        public bool Add { get; set; }
        public string? Note { get; set; }

        public SetCandidateStatusCommand ToCommand(Guid candidateId) => new SetCandidateStatusCommand(candidateId, (Trainova.Domain.Common.Enums.CandidateStatus)Flags, Add, Note);
    }
}
