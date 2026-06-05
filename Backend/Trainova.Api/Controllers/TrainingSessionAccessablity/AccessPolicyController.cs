using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.TrainingSessionAccessablity.AccessPolicies;
using Trainova.Application.Common.Models;

using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.DeleteAccessPolicy;

namespace Trainova.Api.Controllers.TrainingSessionAccessablity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessPolicyController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController(currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> CreateAccessPolicy([FromBody] CreateAccessPolicyRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAccessPolicy(
            [FromRoute] Guid id,
            [FromBody] UpdateAccessPolicyRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAccessPolicy([FromRoute] Guid id)
        {
            var command = new DeleteAccessPolicyCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                onValue: (done, status) => Success(done, status),
                onError: errors => ErrorsPassed(errors));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchAccessPolicyRequest request)
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return MapResult(result);
        }
    }
}
