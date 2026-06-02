using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Scouting;
using Trainova.Application.Scouting.Candidates.Queries.GetCandidates;
using Trainova.Application.Scouting.Candidates.Commands.CreateCandidate;
using Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate;
using Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus;
using Trainova.Application.Common.Models;
using Trainova.Api.Models;
using Trainova.Application.Scouting.Candidates;
using Trainova.Common.ResultOf;

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
            return MapResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCandidate(Guid id, [FromBody] UpdateCandidateRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPost("{id:guid}/status")]
        public async Task<IActionResult> SetCandidateStatus(Guid id, [FromBody] SetCandidateStatusRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetCandidatesOverview([FromQuery] GetCandidatesFiltrationRequest request)
        {
            var query = request.ToOverviewQuery();
            var result = await _sender.Send(query);
            return MapResult(result);
        }

    }
}
