using System;
using Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus;
using Trainova.Domain.Common.Enums;

namespace Trainova.Api.Requests.Scouting
{
    public class SetCandidateStatusRequest
    {
       
        public CandidateStatus Status { get; set; }
        public bool Add { get; set; } = true;
        public string? Note { get; set; }

        public SetCandidateStatusCommand ToCommand(Guid candidateId) => new SetCandidateStatusCommand(candidateId, (Trainova.Domain.Common.Enums.CandidateStatus)Status, Add, Note);
    }
}
