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
using Trainova.Application.Scouting.Candidates.Commands.AddCandidateNote;
using Trainova.Application.Scouting.Candidates.Queries.GetCandidateNotes;
using Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateNote;

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

        // UpdateCandidate endpoint removed - candidate edits are no longer supported via PUT. Use specific endpoints (status, notes) instead.

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

        [HttpPost("{id:guid}/notes")]
        public async Task<IActionResult> AddCandidateNote(Guid id, [FromBody] AddCandidateNoteRequest request)
        {
            var command = new AddCandidateNoteCommand(id, request.Text);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpGet("{id:guid}/notes")]
        public async Task<IActionResult> GetCandidateNotes(Guid id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 50)
        {
            var query = new GetCandidateNotesQuery(id, pageNumber, pageSize);
            var result = await _sender.Send(query);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}/notes/{noteId:guid}")]
        public async Task<IActionResult> DeleteCandidateNote(Guid id, Guid noteId)
        {
            var command = new DeleteCandidateNoteCommand(id, noteId);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

    }
}
