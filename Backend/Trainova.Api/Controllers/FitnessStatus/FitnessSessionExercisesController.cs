using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.FitnessStatus;
using Trainova.Application.Common.Models;
using Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.DeleteFitnessSessionExercise;
using Trainova.Application.FitnessStatus.FitnessSessionExercises.Queries.GetExercisesBySessionId;

namespace Trainova.Api.Controllers.FitnessStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessSessionExercisesController(
        IMediator _mediator,
        CurrentUser _currentUser)
        : ApiController(_currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFitnessSessionExerciseRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateFitnessSessionExerciseRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _mediator.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteFitnessSessionExerciseCommand(id));
            return MapResult(result);
        }
        [HttpGet("session/{sessionId:guid}")]
        public async Task<IActionResult> GetBySessionId(
            [FromRoute] Guid? sessionId = null)
        {
            var command = new GetFitnessSessionExercisesQuery(sessionId: sessionId);
            var result = await _mediator.Send(command);
            return MapResult(result);
        }
        [HttpGet("exercise/{exerciseId:guid}")]
        public async Task<IActionResult> GetByexerciseId(
            [FromRoute] Guid? exerciseId = null)
        {
            var command = new GetFitnessSessionExercisesQuery(exerciseId: exerciseId);
            var result = await _mediator.Send(command);
            return MapResult(result);
        }
    }
}
