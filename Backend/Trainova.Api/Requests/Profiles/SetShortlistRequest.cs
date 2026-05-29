using Trainova.Application.Profiles.Candidates.Commands.SetCandidateShortlist;

namespace Trainova.Api.Requsts.Profiles
{
    public class SetShortlistRequest
    {
        public bool IsShortlisted { get; init; }

        public SetCandidateShortlistCommand ToCommand(Guid id)
        {
            return new SetCandidateShortlistCommand(id, IsShortlisted);
        }
    }
}
