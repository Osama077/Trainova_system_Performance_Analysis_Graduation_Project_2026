using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tsp;
using Trainova.Api.Requests.MedicalStatus.PlanPhases;
using Trainova.Application.Common.Models;
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
            [FromRoute]Guid playerInjuryId)
        {
            var query = new GetRecoveryPlanPhasesQuery(playerInjuryId);
            var result = await _sender.Send(query);
            return MapResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecoveryPlanPhase(
            [FromBody] CreateRecoveryPlanPhaseRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }



    }
}
