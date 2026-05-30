using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Profiles;
using Trainova.Application.Scouting.Candidates.Queries.GetCandidates;
using Trainova.Application.Scouting.Candidates.Commands.CreateCandidate;
using Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate;
using Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus;
using Trainova.Application.Common.Models;
using Trainova.Api.Models;
using Trainova.Application.Scouting.Candidates;

namespace Trainova.Api.Controllers.Scouting
{
    [Route("api/profiles/candidates")]
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
            var query = request.ToQuery(null);
            var result = await _sender.Send(query);
            var list = result.ToList();

            var response = new ApiResponse<IEnumerable<CandidateListItemResponse>>(list, "Candidates retrieved successfully", 200, default, default, null, list.Count, null);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);

            // Return a structured API response with message and the created candidate id
            var response = new ApiResponse<Guid?>(result, "Scouting candidate created successfully", 201);
            return Created(string.Empty, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCandidate(Guid id, [FromBody] UpdateCandidateRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            if (result == null) return NotFound();

            var response = new ApiResponse<string?>(result, "Candidate updated successfully", 200);
            return Ok(response);
        }

        [HttpPost("{id:guid}/status")]
        public async Task<IActionResult> SetCandidateStatus(Guid id, [FromBody] SetCandidateStatusRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            if (!result) return NotFound();

            var response = new ApiResponse<bool>(true, "Candidate status updated successfully", 200);
            return Ok(response);
        }

    }
}
