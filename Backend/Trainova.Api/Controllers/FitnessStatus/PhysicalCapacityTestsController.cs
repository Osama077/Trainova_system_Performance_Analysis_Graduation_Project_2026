using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.FitnessStatus;
using Trainova.Application.Common.Models;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.DeletePhysicalCapacityTest;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityOverTime;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityTests;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPlayerFitnessMetricsSummary;

namespace Trainova.Api.Controllers.FitnessStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalCapacityTestsController(
        IMediator _mediator,
        CurrentUser _currentUser)
        : ApiController(_currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhysicalCapacityTestRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeletePhysicalCapacityTestCommand(id));
            return MapResult(result);
        }

        [HttpGet("CapacityOverTime")]
        public async Task<IActionResult> GetPhysicalCapacityOverTime(Guid playerId)
        {
            var result = await _mediator.Send(new GetPhysicalCapacityOverTimeQuery(playerId));
            return MapResult(result);
        }
        [HttpGet("{playerId:guid?}")]
        public async Task<IActionResult> GetPhysicalCapacityTests(
            [FromRoute] Guid? playerId = null,
            [FromQuery] string? searchName = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var result = await _mediator.Send(new GetPhysicalCapacityTestsQuery(playerId, searchName, fromDate, toDate));
            return MapResult(result);
        }
        [HttpGet("{playerId:guid}/PlayerFitnessMetric")]
        public async Task<IActionResult> GetPlayerFitnessMetric(
            [FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new GetPlayerFitnessMetricsSummaryQuery(playerId));
            return MapResult(result);
        }

    }
}
