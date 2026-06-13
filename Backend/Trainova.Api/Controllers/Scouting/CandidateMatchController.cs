using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Scouting;
using Trainova.Application.Common.Models;
using Trainova.Application.Scouting.Candidates.Commands.AddCandidateMatch;
using Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateMatch;
using Trainova.Application.Scouting.Candidates.Queries.GetCandidateMatches;

namespace Trainova.Api.Controllers.Scouting
{
    [Route("api/profiles/candidates/{candidateId:guid}/matches")]
    [ApiController]
    public class CandidateMatchController : ApiController
    {
        private readonly ISender _sender;

        public CandidateMatchController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidateMatch(Guid candidateId, [FromBody] AddCandidateMatchRequest request)
        {
            var command = new AddCandidateMatchCommand(
                candidateId,
                request.MatchDate,
                request.MatchName,
                request.Goals,
                request.Assists,
                request.Rating,
                request.ScoutNotes);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidateMatches(Guid candidateId, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 50)
        {
            var query = new GetCandidateMatchesQuery(candidateId, pageNumber, pageSize);
            var result = await _sender.Send(query);
            return MapResult(result);
        }

        [HttpDelete("{matchId:guid}")]
        public async Task<IActionResult> DeleteCandidateMatch(Guid candidateId, Guid matchId)
        {
            var command = new DeleteCandidateMatchCommand(candidateId, matchId);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
    }
}
