using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Models;
using Trainova.Api.Requests.MedicalStatus.PlayerInjuries;
using Trainova.Application.Common.Models;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory;

namespace Trainova.Api.Controllers.MedicalStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerInjuryController(
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
            [FromBody] PlayerInjuryUpdateRequet request)
        {
            var command = request.ToUpdateCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetPlayerInjuries(
            [FromQuery] GetPlayerInjuryFiltrationRequest request
            )
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => ErrorsPassed(errors));

        }
        [HttpGet("trend")]
        public async Task<IActionResult> GetPlayerInjuriesTrend(
            [FromQuery] GetPlayerInjuryTrendFiltrationRequest request
            )
        {
            var query = request.ToTrendQuery();
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (injury, status) =>Success(injury, status),
                onError: errors => ErrorsPassed(errors));

        }
        [HttpGet("history")]
        public async Task<IActionResult> GetPlayerInjuriesHistory(
            [FromQuery] HistoryRequest pagennator,
            [FromQuery] Guid? playerInjuryId = null
            )
        {
            var query = pagennator.ToPlayerInjuriesHistoryQuery(playerInjuryId);
            var result = await _sender.Send(query);
            return MapResult(result);

        }

        [HttpGet("CountOver")]
        public async Task<IActionResult> GetInjuryStatus(
            [FromQuery] int days = 7,
            [FromQuery] Guid? injuryId = null
            )
        {
            var query = new GetInjuriesCasesCountOverDayesQuery(injuryId, days);
            var result = await _sender.Send(query);
            return result.Match(
                onValue: (done, status) => Success(done, status),
                onError: errors => ErrorsPassed(errors)
                );
        }
    }
}
