using Trainova.Api.Models;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries;

namespace Trainova.Api.Requsts.MedicalStatus.Injuries
{
    public class GetInjuryFiltrationRequest : Paginator
    {
        public string? InjuryType { get; set; }
        public GetInjuriesQuery ToQuery(Guid? id)
        {
            return new GetInjuriesQuery(id, InjuryType, Page, PageSize);
        }
    }
}
