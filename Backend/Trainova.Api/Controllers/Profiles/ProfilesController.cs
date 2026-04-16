using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requsts.Profiles;
using Trainova.Application.Common.Models;

namespace Trainova.Api.Controllers.Profiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ApiController
    {
        private readonly ISender _sender;
        public ProfilesController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlayerProfiles(
            [FromQuery] Guid? playerId,
            [FromQuery] GetProfilesFiltrationRequest request)
        {
            var query = request.ToQuery(playerId);
            var result = await _sender.Send(query);
            return MapResult(result);

        }

    }
}
