using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.MatchsManagement;
using Trainova.Application.Common.Models;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Api.Controllers.MatchsManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateMatchesController : ApiController
    {
        private readonly ISender _sender;
        public CandidateMatchesController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateMatchRequest request)
        {
            var cmd = request.ToCommand();
            var result = await _sender.Send(cmd);
            return MapResult<CandidateMatch>(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCandidateMatchRequest request)
        {
            var cmd = request.ToCommand(id);
            var result = await _sender.Send(cmd);
            return MapResult<CandidateMatch>(result);
        }
    }
}
