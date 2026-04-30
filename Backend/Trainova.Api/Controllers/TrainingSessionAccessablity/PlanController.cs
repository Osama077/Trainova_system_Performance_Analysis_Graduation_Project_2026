using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.TrainingSessionAccessablity.Plans;
using Trainova.Application.Common.Models;
using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan;
using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.DeletePlan;
using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan;

namespace Trainova.Api.Controllers.TrainingSessionAccessablity
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController(currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlan(
            [FromRoute] Guid id,
            [FromBody] UpdatePlanRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlan([FromRoute] Guid id)
        {
            var command = new DeletePlanCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                onValue: (done, status) => Success(done, status),
                onError: errors => ErrorsPassed(errors));
        }
    }
}
