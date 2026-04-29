using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Models;
using Trainova.Api.Requsts.MedicalStatus.Injuries;
using Trainova.Application.Common.Models;
using Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury;

namespace Trainova.Api.Controllers.MedicalStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class InjuryController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController (currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> CreateInjury(
            [FromBody] InjuryRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInjury(
            [FromRoute] Guid id,
            [FromBody] UpdateInjuryRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetInjuries(
            [FromQuery] GetInjuryFiltrationRequest request
            )
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return MapResult(result);
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetInjuriesHistory(
            [FromQuery] HistoryRequest pagennator,
            [FromQuery] Guid? id = null
            )
        {
            var query = pagennator.ToInjuriesHistoryQuery(id);
            var result = await _sender.Send(query);
            return MapResult(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInjuries([FromRoute] Guid id)
        {
            var query = new DeleteInjuryCommand(id);
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (done, status) =>Success(done, status),
                onError: errors => ErrorsPassed(errors));
        }

    }
}
