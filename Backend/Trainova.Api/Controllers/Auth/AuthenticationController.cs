using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Auth;
using Trainova.Application.Authentication.Commands.ConfirmEmail;
using Trainova.Application.Authentication.Commands.CreateToken;
using Trainova.Application.Authentication.Commands.PasswordReset;
using Trainova.Application.Authentication.Queries.Login;
using Trainova.Application.Common.Models;

namespace Trainova.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(
            IMediator mediator,
            CurrentUser _currentUser)
            : base(_currentUser)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var query = request.ToQuery();
            var result = await _mediator.Send(query);
            return MapResult(result);
        }

        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] string token)
        {
            var command = new ConfirmEmailCommand(token);
            var result = await _mediator.Send(command);
            return MapResult(result);

        }
        [HttpPost("Token/{type:alpha}")]
        public async Task<IActionResult> CreateToken(
            [FromRoute] string type,
            [FromBody] string? email = null)
        {
            var command = new CreateTokenCommand(type, email);
            var result = await _mediator.Send(command);
            return MapResult(result);

        }
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] PasswordResesRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return MapResult(result);
        }
    }
}
