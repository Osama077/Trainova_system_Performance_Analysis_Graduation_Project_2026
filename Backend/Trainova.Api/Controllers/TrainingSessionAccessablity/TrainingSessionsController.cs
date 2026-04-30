using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateTrainingSession;

namespace Trainova.Api.Controllers.TrainingSessionAccessablity
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSessionsController : ApiController
    {

        private readonly ISender _mediator;

        public TrainingSessionsController(ISender mediator, CurrentUser currentUser) : base(currentUser)
        {
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> CreateTrainingSession(CreateTrainingSessionRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return MapResult(result);
        }
    }
}
