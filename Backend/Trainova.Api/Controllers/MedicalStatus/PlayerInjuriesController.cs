using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Models;
using Trainova.Api.Requsts.MedicalStatus.Injuries;
using Trainova.Api.Requsts.MedicalStatus.PlayerInjuries;
using Trainova.Application.Common.Models;
using Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory;

namespace Trainova.Api.Controllers.MedicalStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerInjuriesController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController (currentUser)
    {

        
        [HttpPost]
        public async Task<IActionResult> CreatePlayerInjury(
            [FromBody] PlayerInjuryRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);

        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlayerInjury(
            [FromRoute] Guid id,
            [FromBody] PlayerInjuryRequest request)
        {
            var command = request.ToUpdateCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);

        }
        [HttpGet("{injuryId:guid?}")]
        public async Task<IActionResult> GetPlayerInjuries(
            [FromQuery] GetInjuryFiltrationRequest request,
            [FromRoute] Guid? injuryId = null
            )
        {
            var query = request.ToQuery(injuryId);
            var result = await _sender.Send(query);
            return MapResult(result);

        }
        [HttpGet("history")]
        public async Task<IActionResult> GetPlayerInjuriesHistory(
            [FromQuery] HistoryRequest pagennator,
            [FromQuery] Guid? playerInjuryId = null
            )
        {
            var query = new GetPlayerInjuryHistoryQuery(
                playerInjuryId,
                pagennator.Page,
                pagennator.PageSize,
                pagennator.IncludeAdded,
                pagennator.IncludeDeleted,
                pagennator.IncludeUpdated);

            var result = await _sender.Send(query);
            return MapResult(result);

        }


    }
}
