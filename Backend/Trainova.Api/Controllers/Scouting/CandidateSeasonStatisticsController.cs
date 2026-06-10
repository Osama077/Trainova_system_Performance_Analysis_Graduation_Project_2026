using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trainova.Api.Requests.Scouting;
using Trainova.Application.Common.Models;
using Trainova.Application.Scouting.Candidates.Commands.AddSeasonStatistics;
using Trainova.Application.Scouting.Candidates.Commands.DeleteSeasonStatistics;
using Trainova.Application.Scouting.Candidates.Queries.GetSeasonStatistics;

namespace Trainova.Api.Controllers.Scouting
{
    [Route("api/profiles/candidates/{candidateId:guid}/season-statistics")]
    [ApiController]
    public class CandidateSeasonStatisticsController : ApiController
    {
        private readonly ISender _sender;

        public CandidateSeasonStatisticsController(CurrentUser? currentUser, ISender sender) : base(currentUser)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddSeasonStatistics(Guid candidateId, [FromBody] AddSeasonStatisticsRequest request)
        {
            var command = new AddSeasonStatisticsCommand(
                candidateId,
                request.Season,
                request.League,
                request.Goals,
                request.Assists,
                request.Matches,
                request.PassAccuracy,
                request.ShotsPer90,
                request.XgPer90);
            var result = await _sender.Send(command);
            return MapResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasonStatistics(Guid candidateId)
        {
            var query = new GetSeasonStatisticsQuery(candidateId);
            var result = await _sender.Send(query);
            return MapResult(result);
        }

        [HttpDelete("{seasonId:guid}")]
        public async Task<IActionResult> DeleteSeasonStatistics(Guid candidateId, Guid seasonId)
        {
            var command = new DeleteSeasonStatisticsCommand(candidateId, seasonId);
            var result = await _sender.Send(command);
            return MapResult(result);
        }
    }
}
