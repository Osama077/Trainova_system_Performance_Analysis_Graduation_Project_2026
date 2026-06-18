using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Application.Common.Models;
using Trainova.Application.Profiles.Queries.GetPlayerMedicalScreen;

namespace Trainova.Api.Controllers.Profiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ApiController
    {
        private readonly ISender _sender;
        public PlayerController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }
        [HttpGet("PlayerMedicalScreen")]
        public async Task<IActionResult> GetMedicalScreen([FromQuery] Guid? plyerId = null)
        {
            var query = new GetPlayerMedicalScreenQuery(plyerId);
            var result = await _sender.Send(query);
            return MapResult(result);
        }
    }
}
