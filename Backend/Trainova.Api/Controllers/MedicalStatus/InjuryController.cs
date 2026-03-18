using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Models;
using Trainova.Api.Requsts.MedicalStatus.Injuries;
using Trainova.Application.Common.Models;
using Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory;

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
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => Problem(errors));
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInjury(
            [FromRoute] Guid id,
            [FromBody] InjuryRequest request)
        {
            var command = request.ToUpdateCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => Problem(errors));
        }
        [HttpGet("{id:guid?}")]
        public async Task<IActionResult> GetInjuries(
            [FromQuery] GetInjuryFiltrationRequest request,
            [FromRoute] Guid? id = null
            )
        {
            var query = request.ToQuery(id);
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => Problem(errors));
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetInjuriesHistory(
            [FromQuery] HistoryRequest pagennator,
            [FromQuery] Guid? id = null
            )
        {
            var query = new GetInjuriesHistoryQuery(
                id,
                pagennator.Page,
                pagennator.PageSize,
                pagennator.IncludeAdded,
                pagennator.IncludeDeleted,
                pagennator.IncludeUpdated);
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => Problem(errors));
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInjuries([FromRoute] Guid id)
        {
            var query = new DeleteInjuryCommand(id);
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => Problem(errors));
        }

    }
}
