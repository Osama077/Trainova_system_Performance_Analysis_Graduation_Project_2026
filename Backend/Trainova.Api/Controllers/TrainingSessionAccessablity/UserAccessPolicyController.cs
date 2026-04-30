using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.TrainingSessionAccessablity.UserAccessPolicies;
using Trainova.Application.Common.Models;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.CreateUserAccessPolicy;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.DeleteUserAccessPolicy;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.UpdateUserAccessPolicy;

namespace Trainova.Api.Controllers.TrainingSessionAccessablity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessPolicyController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController(currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAccessPolicy([FromBody] CreateUserAccessPolicyRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUserAccessPolicy(
            [FromRoute] Guid id,
            [FromBody] UpdateUserAccessPolicyRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserAccessPolicy([FromRoute] Guid id)
        {
            var command = new DeleteUserAccessPolicyCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                onValue: (done, status) => Success(done, status),
                onError: errors => ErrorsPassed(errors));
        }
    }
}
