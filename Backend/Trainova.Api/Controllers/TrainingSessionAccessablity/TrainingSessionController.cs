using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.TrainingSessionAccessablity.TrainingSessions;
using Trainova.Application.Common.Models;
using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession;
using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.DeleteTrainingSession;
using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession;

namespace Trainova.Api.Controllers.TrainingSessionAccessablity
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSessionController(
        CurrentUser currentUser,
        ISender _sender)
        : ApiController(currentUser)
    {
        [HttpPost]
        public async Task<IActionResult> CreateTrainingSession([FromBody] CreateTrainingSessionRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTrainingSession(
            [FromRoute] Guid id,
            [FromBody] UpdateTrainingSessionRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTrainingSession([FromRoute] Guid id)
        {
            var command = new DeleteTrainingSessionCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                onValue: (done, status) => Success(done, status),
                onError: errors => ErrorsPassed(errors));
        }
    }
}
