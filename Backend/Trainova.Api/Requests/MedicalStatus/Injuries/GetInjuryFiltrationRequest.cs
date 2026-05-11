using Trainova.Api.Models;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries;

namespace Trainova.Api.Requsts.MedicalStatus.Injuries
{
    public class GetInjuryFiltrationRequest
    {
        public string? InjuryType { get; set; }
        public string? SearchTerm { get; set; }
        public GetInjuriesQuery ToQuery()
        {
            return new GetInjuriesQuery(SearchTerm, InjuryType);
        }
    }
}
