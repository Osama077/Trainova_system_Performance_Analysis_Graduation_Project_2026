using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tsp;
using Trainova.Api.Requests.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Application.MedicalStatus.PlanPhases.Commands.DeletePlanPhase;
using Trainova.Application.MedicalStatus.PlanPhases.Queries.GetRecoveryPlanPhases;

namespace Trainova.Api.Controllers.MedicalStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoveryPlanPhasesController(
        ISender _sender,
        CurrentUser? currentUser)
        : ApiController(currentUser)
    {

        [HttpGet("{playerInjuryId:guid}")]
        public async Task<IActionResult> GetRecoveryPlanPhases(
            [FromRoute] Guid playerInjuryId)
        {
            var query = new GetRecoveryPlanPhasesQuery(playerInjuryId);
            var result = await _sender.Send(query);
            return MapResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecoveryPlanPhase(
            [FromBody] RecoveryPlanPhaseCreateRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }
        [HttpPatch("{playerInjuryId}/Resort")]
        public async Task<IActionResult> ResortRecoveryPlanPhases(
            [FromBody] RecoveryPlanPhaseResortRequest request,
            [FromRoute] Guid playerInjuryId)
        {
            var command = request.ToCommand(playerInjuryId);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlanPhase(
            [FromBody] RecoveryPlanPhaseUpdateRequest request,
            [FromRoute] Guid id)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlanPhase(
            [FromRoute] Guid id)
        {
            var command = new DeleteRecoveryPlanPhaseCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
    }
}
