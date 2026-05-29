using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requsts.Profiles;
using Trainova.Application.Profiles.Candidates.Queries.GetCandidates;
using Trainova.Application.Profiles.Candidates.Commands.SetCandidateShortlist;
using Trainova.Application.Common.Models;
using Trainova.Application.MatchsManagement.Matches.Queries.GetCandidateMatches;
using Trainova.Api.Requests.Profiles;
using Trainova.Application.Profiles.Candidates.Commands.CreateCandidate;

namespace Trainova.Api.Controllers.Profiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ApiController
    {
        private readonly ISender _sender;
        public CandidateController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidates([FromQuery] GetCandidatesFiltrationRequest request)
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return MapResult(result);
        }

        [HttpPut("{id:guid}/shortlist")]
        public async Task<IActionResult> SetShortlist([FromRoute] Guid id, [FromBody] SetShortlistRequest request)
        {
            var cmd = request.ToCommand(id);
            var result = await _sender.Send(cmd);
            return MapResult(result);
        }

        [HttpGet("{id:guid}/matches")]
        public async Task<IActionResult> GetCandidateMatches([FromRoute] Guid id, [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 12)
        {
            var query = new GetCandidateMatchesQuery()
            {
                CandidateId = id,
                DateFrom = dateFrom,
                DateTo = dateTo,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _sender.Send(query);
            return MapResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateRequest request)
        {
            var cmd = request.ToCommand();
            var result = await _sender.Send(cmd);
            return MapResult<Trainova.Domain.Profiles.Candidate>(result);
        }
    }
}
