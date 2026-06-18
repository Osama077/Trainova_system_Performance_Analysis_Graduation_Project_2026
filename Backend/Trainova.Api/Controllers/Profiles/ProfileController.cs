using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Profiles;
using Trainova.Api.Requsts.Profiles;
using Trainova.Application.Common.Models;
using Trainova.Application.Profiles.Queries.GetSquadHealthProfiles;
using Trainova.Application.Profiles.Queries.GetTeamPlayersFitness;

namespace Trainova.Api.Controllers.Profiles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ApiController
    {
        private readonly ISender _sender;
        public ProfileController(CurrentUser? currentUser, ISender sender) : base(currentUser)
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
        [HttpGet("MedicalAnalytics")]
        public async Task<IActionResult> GetMedicalStatus(
            [FromQuery] GetSquadHealthProfilesQuery request)
        {
            var result = await _sender.Send(request);
            return MapResult(result);
        }
        [HttpGet("FitnessAnalytics")]
        public async Task<IActionResult> GetFitnessStatus(
            [FromQuery] GetTeamPlayersFitnessGridQuery request)
        {
            var result = await _sender.Send(request);
            return MapResult(result);
        }
        [HttpPost("PlayerProfiles")]
        public async Task<IActionResult> GetPlayerProfilesByIds([FromBody] CreatePlayerProfilesRequest request)
        {
            var result = await _sender.Send(request.ToCommand());
            return MapResult(result);
        }
        [HttpPost("TeamStaffProfiles")]
        public async Task<IActionResult> GetTeamStaffProfilesByIds([FromBody] CreateTeamStaffProfilesRequest request)
        {
            var result = await _sender.Send(request.ToCommand());
            return MapResult(result);
        }
    }
}
