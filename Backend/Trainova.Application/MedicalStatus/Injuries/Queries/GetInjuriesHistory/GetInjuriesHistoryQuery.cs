using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory
{
    public record GetInjuriesHistoryQuery(
        Guid? Id,
        int Page = 0,
        int PageSize = 12,
        bool IncludeAdded = false,
        bool IncludeDeleted = false,
        bool IncludeUpdated = false)
        : IRequest<ResultOf<IEnumerable<AuditLog>>>;

}