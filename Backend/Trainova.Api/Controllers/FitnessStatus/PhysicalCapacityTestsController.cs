using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.FitnessStatus;
using Trainova.Application.Common.Models;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.DeletePhysicalCapacityTest;

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
    }
}
