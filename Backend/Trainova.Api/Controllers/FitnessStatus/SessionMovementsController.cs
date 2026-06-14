using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.FitnessStatus;
using Trainova.Application.Common.Models;
using Trainova.Application.FitnessStatus.SessionMovements.Commands.DeleteSessionMovement;
using Trainova.Application.FitnessStatus.SessionMovements.Queries.PlayerLoadVsCapacityTimeline;

namespace Trainova.Api.Controllers.FitnessStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionMovementsController(
        IMediator _mediator,
        CurrentUser _currentUser)
        : ApiController(_currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionMovementRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateSessionMovementRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _mediator.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteSessionMovementCommand(id));
            return MapResult(result);
        }
        [HttpGet("{playerId:guid}/PlayerLoadVsCapacityTimeline")]
        public async Task<IActionResult> GetPlayerLoadVsCapacityTimeline(
            [FromRoute] Guid playerId,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var result = await _mediator.Send(new GetPlayerLoadVsCapacityTimelineQuery(playerId, fromDate, toDate));
            return MapResult(result);
        }
    }
}
